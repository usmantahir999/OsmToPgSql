using Api.Dtos.Request;
using Api.Dtos.Response;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoadNetworkController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public RoadNetworkController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("bbox")]
        public async Task<IActionResult> GetFeaturesInBoundingBox([FromBody] BoundingBoxRequest request)
        {
            var result = new BboxFeaturesResponse();
            var connStr = _configuration.GetConnectionString("Database");

            await using var conn = new NpgsqlConnection(connStr);
            await conn.OpenAsync();

            // ----------------- Edges Query -----------------
            var edgeCmd = conn.CreateCommand();
            edgeCmd.CommandText = @"
            SELECT osm_id, name, highway, ST_AsGeoJSON(way)
            FROM planet_osm_line
            WHERE ST_Intersects(
              way,
              ST_Transform(
                ST_MakeEnvelope(@minLon, @minLat, @maxLon, @maxLat, 4326),
                ST_SRID(way)
              )
            );
        ";
            edgeCmd.Parameters.AddWithValue("minLat", request.MinLat);
            edgeCmd.Parameters.AddWithValue("minLon", request.MinLon);
            edgeCmd.Parameters.AddWithValue("maxLat", request.MaxLat);
            edgeCmd.Parameters.AddWithValue("maxLon", request.MaxLon);

            await using (var reader = await edgeCmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    result.Edges.Add(new OsmEdge
                    {
                        OsmId = reader.GetInt64(0),
                        Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                        Highway = reader.IsDBNull(2) ? null : reader.GetString(2),
                        Geometry = reader.GetString(3)
                    });
                }
            }

            // --- Query Nodes ---
            var nodeCmd = conn.CreateCommand();
            nodeCmd.CommandText = @"
            SELECT osm_id, name, ST_AsGeoJSON(way)
            FROM planet_osm_point
            WHERE ST_Intersects(
              way,
              ST_Transform(
                ST_MakeEnvelope(@minLon, @minLat, @maxLon, @maxLat, 4326),
                ST_SRID(way)
              )
            );
        ";
            nodeCmd.Parameters.AddWithValue("minLat", request.MinLat);
            nodeCmd.Parameters.AddWithValue("minLon", request.MinLon);
            nodeCmd.Parameters.AddWithValue("maxLat", request.MaxLat);
            nodeCmd.Parameters.AddWithValue("maxLon", request.MaxLon);

            await using (var reader = await nodeCmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    result.Nodes.Add(new OsmNode
                    {
                        OsmId = reader.GetInt64(0),
                        Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                        Geometry = reader.GetString(2)
                    });
                }
            }

            return Ok(result);
        }
    }
}

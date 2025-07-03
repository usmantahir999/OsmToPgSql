namespace Api.Dtos.Response
{
    public class OsmEdge
    {
        public long OsmId { get; set; }
        public string? Name { get; set; }
        public string? Highway { get; set; }
        public string Geometry { get; set; } = null!;
    }
}

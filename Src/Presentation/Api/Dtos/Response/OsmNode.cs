namespace Api.Dtos.Response
{
    public class OsmNode
    {
        public long OsmId { get; set; }
        public string? Name { get; set; }
        public string Geometry { get; set; } = null!;
    }
}

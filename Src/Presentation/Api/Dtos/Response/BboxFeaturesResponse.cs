namespace Api.Dtos.Response
{
    public class BboxFeaturesResponse
    {
        public List<OsmNode> Nodes { get; set; } = new();
        public List<OsmEdge> Edges { get; set; } = new();
    }
}

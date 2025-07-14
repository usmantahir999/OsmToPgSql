namespace RoadNetworkService.Application.Features.Queries.GetGraphByBoundingBox
{
    public class BboxFeaturesResponse
    {
        public List<OsmNodeDto> Nodes { get; set; } = new();
        public List<OsmEdgeDto> Edges { get; set; } = new();
    }
}

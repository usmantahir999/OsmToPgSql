namespace RoadNetworkService.Application.Features.Queries.GetGraphByBoundingBox
{
    public record BoundingBoxRequest(
    double MinLat,
    double MinLon,
    double MaxLat,
    double MaxLon
);
}

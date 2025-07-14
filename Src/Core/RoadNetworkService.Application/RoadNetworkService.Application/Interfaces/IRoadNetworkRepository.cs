using RoadNetworkService.Application.Features.Queries.GetEdgeConnectingNodes;
namespace RoadNetworkService.Application.Interfaces
{
    public interface IRoadNetworkRepository
    {
        Task<BboxFeaturesResponse> GetGraphByBoundingBox(BoundingBoxRequest boundingBox, CancellationToken cancellationToken);
        Task<GetEdgeConnectingNodesResponse> GetEdgeConnectingNodes(EdgeBetweenNodesRequest EdgeBetweenNodes, CancellationToken cancellationToken);
    }
}

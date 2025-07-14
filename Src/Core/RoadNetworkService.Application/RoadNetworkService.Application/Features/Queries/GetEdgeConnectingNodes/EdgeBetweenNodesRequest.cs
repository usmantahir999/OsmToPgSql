namespace RoadNetworkService.Application.Features.Queries.GetEdgeConnectingNodes
{
    public record EdgeBetweenNodesRequest(long NodeId1, long NodeId2);
}

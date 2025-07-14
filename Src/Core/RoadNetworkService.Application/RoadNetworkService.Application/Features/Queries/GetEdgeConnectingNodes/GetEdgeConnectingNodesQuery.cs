namespace RoadNetworkService.Application.Features.Queries.GetEdgeConnectingNodes
{
    public class GetEdgeConnectingNodesQuery :IRequest<IResponseWrapper>
    {
        public EdgeBetweenNodesRequest Request { get; set; }
    }
}

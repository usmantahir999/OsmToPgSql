
namespace RoadNetworkService.Application.Features.Queries.GetEdgeConnectingNodes
{
    public class GetEdgeConnectingNodesHandler(IRoadNetworkRepository repository) : IRequestHandler<GetEdgeConnectingNodesQuery, IResponseWrapper>
    {
        private readonly IRoadNetworkRepository _repository = repository;

        public async Task<IResponseWrapper> Handle(GetEdgeConnectingNodesQuery query, CancellationToken cancellationToken)
        {
            var result =await _repository.GetEdgeConnectingNodes(query.Request, cancellationToken);
            return await ResponseWrapper<GetEdgeConnectingNodesResponse>.SuccessAsync(result);
        }
    }
}

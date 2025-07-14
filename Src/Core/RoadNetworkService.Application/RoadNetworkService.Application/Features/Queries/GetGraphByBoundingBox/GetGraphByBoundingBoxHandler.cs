namespace RoadNetworkService.Application.Features.Queries.GetGraphByBoundingBox
{
    public class GetGraphByBoundingBoxHandler(IRoadNetworkRepository repository) : IRequestHandler<GetGraphByBoundingBoxQuery, IResponseWrapper>
    {
        private readonly IRoadNetworkRepository _repository = repository;

        public async Task<IResponseWrapper> Handle(GetGraphByBoundingBoxQuery query, CancellationToken cancellationToken)
        {
            var result = await _repository.GetGraphByBoundingBox(query.Request, cancellationToken);
            return await ResponseWrapper<BboxFeaturesResponse>.SuccessAsync(result);
        }
    }
}

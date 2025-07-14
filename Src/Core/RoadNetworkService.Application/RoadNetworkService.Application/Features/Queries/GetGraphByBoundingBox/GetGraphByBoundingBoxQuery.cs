using MediatR;
namespace RoadNetworkService.Application.Features.Queries.GetGraphByBoundingBox
{
    public class GetGraphByBoundingBoxQuery : IRequest<IResponseWrapper>
    {
        public BoundingBoxRequest Request { get; set; }
    }
}

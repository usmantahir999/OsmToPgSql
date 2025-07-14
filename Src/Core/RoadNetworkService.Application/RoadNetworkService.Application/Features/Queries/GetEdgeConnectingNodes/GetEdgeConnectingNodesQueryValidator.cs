

namespace RoadNetworkService.Application.Features.Queries.GetEdgeConnectingNodes
{
    public class GetEdgeConnectingNodesQueryValidator : AbstractValidator<GetEdgeConnectingNodesQuery>
    {
        public GetEdgeConnectingNodesQueryValidator()
        {
            RuleFor(x => x.Request)
                .NotNull()
                .WithMessage(string.Format(ValidationMessages.RequestObjectCannotBeNull, nameof(GetEdgeConnectingNodesQuery.Request)))
                .SetValidator(new EdgeBetweenNodesRequestValidator());
        }
    }

    public class EdgeBetweenNodesRequestValidator : AbstractValidator<EdgeBetweenNodesRequest>
    {
        public EdgeBetweenNodesRequestValidator()
        {
            RuleFor(x => x.NodeId1)
                .GreaterThan(0)
                .WithMessage(string.Format(ValidationMessages.NodeIdMustBeGreaterThanZero, nameof(EdgeBetweenNodesRequest.NodeId1)));

            RuleFor(x => x.NodeId2)
                .GreaterThan(0)
                .WithMessage(string.Format(ValidationMessages.NodeIdMustBeGreaterThanZero, nameof(EdgeBetweenNodesRequest.NodeId2)));

            RuleFor(x => x)
                .Must(x => x.NodeId1 != x.NodeId2)
                .WithMessage(ValidationMessages.NodeIdsMustBeDifferent);
        }
    }
}

namespace RoadNetworkService.Application.Features.Queries.GetGraphByBoundingBox
{
    public class GetGraphByBoundingBoxQueryValidator :AbstractValidator<GetGraphByBoundingBoxQuery>
    {
        public GetGraphByBoundingBoxQueryValidator()
        {
            RuleFor(x => x.Request)
            .NotNull()
            .WithMessage(string.Format(ValidationMessages.RequestObjectCannotBeNull, nameof(GetGraphByBoundingBoxQuery.Request)))
            .SetValidator(new BoundingBoxRequestValidator());
        }

        public class BoundingBoxRequestValidator : AbstractValidator<BoundingBoxRequest>
        {
            public BoundingBoxRequestValidator()
            {
                RuleFor(x => x.MinLat)
                    .InclusiveBetween(-90, 90)
                    .WithMessage(string.Format(ValidationMessages.LatitudeMustBeBetween, nameof(BoundingBoxRequest.MinLat)));

                RuleFor(x => x.MaxLat)
                    .InclusiveBetween(-90, 90)
                    .WithMessage(string.Format(ValidationMessages.LatitudeMustBeBetween, nameof(BoundingBoxRequest.MaxLat)));

                RuleFor(x => x.MinLon)
                    .InclusiveBetween(-180, 180)
                    .WithMessage(string.Format(ValidationMessages.LongitudeMustBeBetween, nameof(BoundingBoxRequest.MinLon)));

                RuleFor(x => x.MaxLon)
                    .InclusiveBetween(-180, 180)
                    .WithMessage(string.Format(ValidationMessages.LongitudeMustBeBetween, nameof(BoundingBoxRequest.MaxLon)));

                RuleFor(x => x)
                    .Must(x => x.MinLat < x.MaxLat && x.MinLon < x.MaxLon)
                    .WithMessage(ValidationMessages.BoundingBoxInvalid);
            }
        }
    }
}

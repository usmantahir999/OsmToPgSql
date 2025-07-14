namespace RoadNetworkService.Application.Common.Constants
{
    public static class ValidationMessages
    {
        public const string NodeIdMustBeGreaterThanZero = "{0} must be greater than 0.";
        public const string NodeIdsMustBeDifferent = "NodeId1 and NodeId2 must not be the same.";
        public const string RequestObjectCannotBeNull = "{0} cannot be null.";

        public const string LatitudeMustBeBetween = "{0} must be between -90 and 90.";
        public const string LongitudeMustBeBetween = "{0} must be between -180 and 180.";
        public const string BoundingBoxInvalid = "MinLat must be less than MaxLat and MinLon must be less than MaxLon.";
    }
}

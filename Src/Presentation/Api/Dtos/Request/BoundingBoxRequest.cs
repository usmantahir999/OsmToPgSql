namespace Api.Dtos.Request
{
    public class BoundingBoxRequest
    {
        public double MinLat { get; set; }
        public double MinLon { get; set; }
        public double MaxLat { get; set; }
        public double MaxLon { get; set; }
    }
}

namespace RoadNetworkService.Application.Dtos
{
    public class OsmEdgeDto
    {
        public long OsmId { get; set; }
        public string Name { get; set; }
        public string Highway { get; set; }
        public string Geometry { get; set; }
    }
}

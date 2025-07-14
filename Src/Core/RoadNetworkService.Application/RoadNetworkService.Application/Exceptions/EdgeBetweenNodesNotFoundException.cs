namespace RoadNetworkService.Application.Exceptions
{
    public class EdgeBetweenNodesNotFoundException : Exception
    {
        public long NodeId1 { get; }
        public long NodeId2 { get; }

        public EdgeBetweenNodesNotFoundException(long nodeId1, long nodeId2)
            : base($"No edge found connecting the given nodes. Node IDs: {nodeId1}, {nodeId2}")
        {
            NodeId1 = nodeId1;
            NodeId2 = nodeId2;
        }
    }
}

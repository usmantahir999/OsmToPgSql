namespace RoadNetworkService.Application.Exceptions
{
    public class NodePairNotFoundException : Exception
    {
        public long NodeId1 { get; }
        public long NodeId2 { get; }

        public NodePairNotFoundException(long nodeId1, long nodeId2)
            : base($"One or both nodes were not found. Node IDs: {nodeId1}, {nodeId2}")
        {
            NodeId1 = nodeId1;
            NodeId2 = nodeId2;
        }
    }
}

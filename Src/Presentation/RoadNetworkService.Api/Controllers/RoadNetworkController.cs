namespace RoadNetworkService.Api.Controllers
{
    public class RoadNetworkController : BaseApiController
    {

        [HttpPost("bbox")]
        public async Task<IActionResult> GetGraphByBoundingBox([FromBody] BoundingBoxRequest request, CancellationToken cancellationToken)
        {
            var response = await Sender.Send(new GetGraphByBoundingBoxQuery { Request = request }, cancellationToken);
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return NotFound(response);
        }


        [HttpPost("edge-between-nodes")]
        public async Task<IActionResult> GetEdgeConnectingNodes([FromBody] EdgeBetweenNodesRequest request, CancellationToken cancellationToken)
        {

            var response = await Sender.Send(new GetEdgeConnectingNodesQuery { Request = request },cancellationToken);
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

    }
}

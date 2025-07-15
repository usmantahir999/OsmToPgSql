namespace RoadNetworkService.Api.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                var response = context.Response;
                response.ContentType = "application/json";

                var responseWrapper = ResponseWrapper.Fail();

                switch (ex)
                {
                    case NodePairNotFoundException npf:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        responseWrapper.Messages = [npf.Message];
                        break;
                    case EdgeBetweenNodesNotFoundException ebnf:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        responseWrapper.Messages = [ebnf.Message];
                        break;
                    case ConflictException ce:
                        response.StatusCode = (int)ce.StatusCode;
                        responseWrapper.Messages = ce.ErrorMessages;
                        break;
                    case NotFoundException nfe:
                        response.StatusCode = (int)nfe.StatusCode;
                        responseWrapper.Messages = nfe.ErrorMessages;
                        break;
                    case ForbiddenException fe:
                        response.StatusCode = (int)fe.StatusCode;
                        responseWrapper.Messages = fe.ErrorMessages;
                        break;

                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        responseWrapper.Messages = [ex.Message];
                        break;
                }

                var result = JsonSerializer.Serialize(responseWrapper);

                await response.WriteAsync(result);
            }
        }
    }
}

namespace RoadNetworkService.Application.Exceptions
{
    public class ConflictException : Exception
    {
        public List<string> ErrorMessages { get; set; } = null!;
        public HttpStatusCode StatusCode { get; set; }
        public ConflictException(List<string> errorMessages = default!, HttpStatusCode statusCode = HttpStatusCode.Conflict)
        {
            StatusCode = statusCode;
            ErrorMessages = errorMessages;
        }
    }
}

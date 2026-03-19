namespace ExpenseManagement.API.Core.Middlewares.Models
{
    public sealed class HttpError
    {
        public HttpStatusCode StatusCode { get; init; } = HttpStatusCode.InternalServerError;

        public string Message { get; init; } = "Internal Server error.";
    }
}

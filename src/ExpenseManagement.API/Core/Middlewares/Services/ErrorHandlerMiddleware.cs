using ExpenseManagement.Domain.Core.Exceptions;

namespace ExpenseManagement.API.Core.Middlewares.Services
{
    public sealed class ErrorHandlerMiddleware : IErrorHandlerMiddleware
    {
        #region Private variables
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;
        #endregion

        #region Constructors
        public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }
        #endregion

        #region Public methods
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BadRequestException ex)
            {
                await HandleExceptionAsync(context, ex, BadRequestException.HttpStatusCode, ex.Errors);
            }
            catch (UnauthorizedException ex)
            {
                await HandleExceptionAsync(context, ex, UnauthorizedException.HttpStatusCode, ex.Errors);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError, []);
            }
        }
        #endregion

        #region Private methods
        private async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception,
            HttpStatusCode httpStatusCode,
            HashSet<string> errors)
        {
            _logger.LogError($"An HTTP {(int)httpStatusCode} {httpStatusCode} error occurred. Message: {exception.Message}");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpStatusCode;

            HttpError httpError = new()
            {
                StatusCode = httpStatusCode,
                Message = errors.Any() ? string.Join(";", errors) : exception.Message
            };

            var response = JsonConvert.SerializeObject(httpError);

            await context.Response.WriteAsync(response);
        }
        #endregion
    }
}

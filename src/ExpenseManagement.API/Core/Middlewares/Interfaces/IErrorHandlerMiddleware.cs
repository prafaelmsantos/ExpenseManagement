namespace ExpenseManagement.API.Core.Middlewares.Interfaces
{
    public interface IErrorHandlerMiddleware
    {
        Task InvokeAsync(HttpContext context);
    }
}

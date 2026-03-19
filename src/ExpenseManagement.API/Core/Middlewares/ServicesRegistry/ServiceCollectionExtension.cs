namespace ExpenseManagement.API.Core.Middlewares.ServicesRegistry
{
    public static class ServiceCollectionExtension
    {
        public static IApplicationBuilder AddErrorHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();

            return app;
        }
    }
}

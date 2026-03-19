namespace ExpenseManagement.API.Core.HealthChecks.ServicesRegistry
{
    public static class ServiceCollectionExtension
    {
        #region Public methods
        public static IApplicationBuilder MapHealthChecksEndpoint(this IApplicationBuilder app)
        {
            if (IsHealthChecksDisabled())
            {
                return app;
            }
            app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                Predicate = _ => true, // run all health checks
                ResponseWriter = JsonResponseWriter.WriteJsonResponseAsync
            });
            return app;
        }

        private static bool IsHealthChecksDisabled()
        {
            return Environment.GetEnvironmentVariable("HEALTH_CHECKS_ENABLED")?.ToUpperInvariant() == "FALSE";
        }
        #endregion
    }
}


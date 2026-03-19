namespace ExpenseManagement.API.Core.Swagger.ServicesRegistry
{
    public static class ServiceCollectionExtension
    {
        #region Public methods
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddSwaggerSettingsOptions();

            using ServiceProvider serviceProvider = services.BuildServiceProvider();
            SwaggerSettings swaggerSettings = serviceProvider.GetRequiredService<IOptions<SwaggerSettings>>().Value;

            if (!swaggerSettings.Enabled)
            {
                return services;
            }

            return services
                .ConfigureOptions<ConfigureSwaggerOptions>()
                .AddSwaggerGen();
        }

        public static IApplicationBuilder UseSwaggerDocs(this WebApplication app, string host)
        {
            SwaggerSettings swaggerSettings = app.Services
                .GetRequiredService<IOptions<SwaggerSettings>>().Value;

            if (!swaggerSettings.Enabled)
            {
                return app;
            }

            return app
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    var apiVersionDescriptions = app.DescribeApiVersions();

                    foreach (ApiVersionDescription description in apiVersionDescriptions)
                    {
                        options.SwaggerEndpoint(
                            $"{host}/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }

                    options.DocumentTitle = swaggerSettings.Title;

                    // Disable swagger schemas at bottom
                    options.DefaultModelsExpandDepth(-1);
                });
        }
        #endregion

        #region Private methods
        private static void AddSwaggerSettingsOptions(this IServiceCollection services)
        {
            services
                .ConfigureOptions<SwaggerSettingsSetup>()
                .AddOptionsWithValidateOnStart<SwaggerSettings, SwaggerSettingsValidator>();
        }
        #endregion
    }
}

namespace ExpenseManagement.API.Core.ServicesRegistry
{
    public static class ServiceCollectionExtension
    {
        #region Public methods
        public static IServiceCollection AddAPIControllerServices(this IServiceCollection services, int majorVersion)
        {
            services
                .AddEndpointsApiExplorer()
                .AddApiVersioning(opt =>
                {
                    opt.DefaultApiVersion = new ApiVersion(majorVersion, 0);
                    opt.AssumeDefaultVersionWhenUnspecified = true;
                    opt.ReportApiVersions = true;
                    opt.ApiVersionReader = ApiVersionReader.Combine(
                        new UrlSegmentApiVersionReader(),
                        new HeaderApiVersionReader("x-api-version"),
                        new MediaTypeApiVersionReader("x-api-version"));
                })
                .AddMvc() // This is needed for controllers
                .AddApiExplorer(setup =>
                {
                    setup.GroupNameFormat = "'v'VVV";
                    setup.SubstituteApiVersionInUrl = true;
                })
                .EnableApiVersionBinding();

            services
                .AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.Formatting = Formatting.None;
                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    JsonConvert.DefaultSettings = () => options.SerializerSettings;
                });

            return services;
        }
        #endregion
    }
}


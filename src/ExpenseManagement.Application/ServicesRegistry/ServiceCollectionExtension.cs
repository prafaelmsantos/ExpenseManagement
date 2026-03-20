namespace ExpenseManagement.Application.ServicesRegistry
{
    public static class ServiceCollectionExtension
    {
        #region Public methods
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddJWTSettingsOptions();
            services.AddJwtBearerOptions();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ISignInService, SignInService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IInitialSyncService, InitialSyncService>();

            return services;
        }
        #endregion

        #region Private methods
        private static void AddJWTSettingsOptions(this IServiceCollection services)
        {
            services
                .ConfigureOptions<JwtSettingsSetup>()
                .AddOptionsWithValidateOnStart<JwtSettings, JwtSettingsValidator>();
        }

        private static void AddJwtBearerOptions(this IServiceCollection services)
        {
            services.ConfigureOptions<JwtBearerOptionsSetup>();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();
        }
        #endregion
    }
}

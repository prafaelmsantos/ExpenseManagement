namespace ExpenseManagement.Application.ServicesRegistry
{
    public static class ServiceCollectionExtension
    {
        #region Public methods
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddJWTSettingsOptions();
            services.AddJwtBearerOptions();
            services.AddUserDefaultSettingsOptions();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IInitialSyncService, InitialSyncService>();
            services.AddScoped<ISavingService, SavingService>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IStatisticService, StatisticService>();

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

        private static void AddUserDefaultSettingsOptions(this IServiceCollection services)
        {
            services
                .ConfigureOptions<UserDefaultSettingsSetup>()
                .AddOptionsWithValidateOnStart<UserDefaultSettings, UserDefaultSettingsValidator>();
        }
        #endregion
    }
}

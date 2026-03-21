namespace ExpenseManagement.Persistence.ServicesRegistry
{
    public static class ServiceCollectionExtension
    {
        #region Public methods
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            services.AddAppSettingsOptions();

            services.AddDbContextServices();

            services
                .AddIdentityCore<User>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;
                })
                .AddRoles<Role>()
                .AddSignInManager<SignInManager<User>>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            // .AddIdentity<User, Role>(x =>
            // {
            //     x.Password.RequireDigit = false;
            //     x.Password.RequireNonAlphanumeric = false;
            //     x.Password.RequireLowercase = false;
            //     x.Password.RequireUppercase = false;
            //     x.Password.RequiredLength = 6;
            // })
            // .AddRoles<Role>()
            // //.AddRoleManager<RoleManager<Role>>()
            // //.AddSignInManager<SignInManager<User>>()
            // //.AddRoleValidator<RoleValidator<Role>>()
            // .AddEntityFrameworkStores<AppDbContext>()
            // .AddDefaultTokenProviders();

            services
                .AddHealthChecks()
                .AddSqlServer(
                    connectionStringFactory: (provider) =>
                    {
                        AppSettings appSettings = provider.GetRequiredService<IOptions<AppSettings>>().Value;
                        return appSettings.ConnectionStrings.Db;
                    },
                    healthQuery: "SELECT 1",
                    configure: null,
                    name: "LoadManagement.PostgreSQL",
                    failureStatus: HealthStatus.Unhealthy,
                    tags: ["db", "sql"],
                    timeout: TimeSpan.FromSeconds(3));

            return services;
        }
        #endregion

        #region Private methods

        private static void AddAppSettingsOptions(this IServiceCollection services)
        {
            services
                .ConfigureOptions<AppSettingsSetup>()
                .AddOptionsWithValidateOnStart<AppSettings, AppSettingsValidator>();
        }

        private static void AddDbContextServices(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>((provider, options) =>
            {
                AppSettings appSettings = provider.GetRequiredService<IOptions<AppSettings>>().Value;

                options.UseLazyLoadingProxies()
                    .CreateDatabaseLogFilter(appSettings.ConnectionStrings.EnableLogging)
                    .UseSqlServer(appSettings.ConnectionStrings.Db, sqlOptions =>
                    {
                        string migrationsAssembly = typeof(AppDbContext)
                            .GetTypeInfo().Assembly
                            .GetName().Name!;

                        sqlOptions.MigrationsAssembly(migrationsAssembly);
                        sqlOptions.EnableRetryOnFailure(
                           maxRetryCount: appSettings.ConnectionStrings.MaxRetryCount,
                           maxRetryDelay: TimeSpan.FromSeconds(appSettings.ConnectionStrings.MaxRetryDelay),
                           errorNumbersToAdd: null);
                    });
                // It's a Microsoft recommendation to implement both methods
                // https://learn.microsoft.com/en-us/ef/core/modeling/data-seeding#use-seeding-method
                //.UseSeeding(DatabaseSeed.Seed)
                //.UseAsyncSeeding(DatabaseSeed.SeedAsync);
            }, ServiceLifetime.Scoped);
        }

        private static DbContextOptionsBuilder CreateDatabaseLogFilter(
            this DbContextOptionsBuilder options,
            bool enableLogging)
        {
            options.EnableSensitiveDataLogging(enableLogging);

            if (!enableLogging)
            {
                options.UseLoggerFactory(LoggerFactory.Create(b =>
                {
                    b.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.None);
                }));
            }

            return options;
        }

        #endregion
    }
}

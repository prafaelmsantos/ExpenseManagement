namespace ExpenseManagement.API
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class Program
    {
        const string CustomPolicy = "CustomPolicy";
        const string TokenExpiredHeader = "Token-Expired";
        const int MajorVersion = 1;

        public static async Task Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);
                ConfigureHost(builder);
                ConfigureServices(builder);

                var app = builder.Build();
                ConfigureApp(app);

                //await RunMigrationsAsync(app);

                await app.RunAsync();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static void ConfigureHost(WebApplicationBuilder builder)
        {
            builder.WebHost.KestrelConfig();

            builder.Host.UseSerilog((context, configuration) =>
            {
                configuration.ReadFrom.Configuration(context.Configuration);
            });
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceServices();

            builder.Services.AddAPIControllerServices(MajorVersion);
            builder.Services.AddSwaggerServices();

            builder.Services.AddCors(o => o.AddPolicy(CustomPolicy, builder =>
            {
                builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .WithExposedHeaders(TokenExpiredHeader);
            }));
        }

        private static void ConfigureApp(WebApplication app)
        {
            string host = GetHost(app);

            app.UsePathBase(host);
            app.UseCors(CustomPolicy);

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.AddErrorHandlerMiddleware();
            app.UseSwaggerDocs(host);

            //app.UseHttpsRedirection();
            app.UseRouting();

            app.MapFallbackToFile("index.html");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapGet($"{host}/", async context => await context.Response.WriteAsync("There is http communication endpoints."));

            app.UseSwaggerDocs(host);

            app.MapHealthChecksEndpoint();
        }

        private static string GetHost(IApplicationBuilder app)
        {
            AppSettings appSettings = app.ApplicationServices
                .GetRequiredService<IOptions<AppSettings>>().Value;

            var host = "/" + appSettings.BasePath.ToLower();

            return host;
        }

        private static async Task RunMigrationsAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<AppDbContext>();
                Console.WriteLine("Update database started");
                context.Database.SetCommandTimeout(TimeSpan.FromHours(2));
                await context.Database.MigrateAsync();
                Console.WriteLine("Update database ended");
            }
        }
    }
}

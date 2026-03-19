namespace ExpenseManagement.API.Core.Host
{
    public static class KestrelExtensions
    {
        public static IWebHostBuilder KestrelConfig(this IWebHostBuilder builder)
        {
            builder.ConfigureKestrel((context, serverOptions) =>
            {
                serverOptions.AddServerHeader = false;

                int httpPort = context.Configuration.GetValue<int?>("HttpPort") ?? 80;

                serverOptions.ListenAnyIP(httpPort, listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http1;
                });
            });

            return builder;
        }
    }
}

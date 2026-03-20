using ExpenseManagement.Application.Services;

namespace ExpenseManagement.Application.ServicesRegistry
{
    public static class ServiceCollectionExtension
    {
        #region Public methods
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IInitialSyncService, InitialSyncService>();
            //services.AddScoped<IOrderService, OrderService>();
            //services.AddScoped<IProductService, ProductService>();

            return services;
        }
        #endregion
    }
}

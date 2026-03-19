namespace ExpenseManagement.API.Core.Swagger.Options
{
    internal sealed class SwaggerSettingsSetup : IConfigureOptions<SwaggerSettings>
    {
        #region Private variables
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructors
        public SwaggerSettingsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Public methods
        public void Configure(SwaggerSettings swaggerSettings)
        {
            _configuration
                .GetSection("Swagger")
                .Bind(swaggerSettings);
        }
        #endregion
    }
}

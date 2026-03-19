namespace ExpenseManagement.Persistence.Options.App
{
    internal sealed class AppSettingsSetup : IConfigureOptions<AppSettings>
    {
        #region Private variables
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructors
        public AppSettingsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Public methods
        public void Configure(AppSettings appSettings)
        {
            _configuration
                .GetSection("AppBase")
                .Bind(appSettings);
        }
        #endregion
    }
}

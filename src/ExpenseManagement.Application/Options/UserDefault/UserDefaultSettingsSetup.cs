namespace ExpenseManagement.Application.Options.UserDefault
{
    internal sealed class UserDefaultSettingsSetup : IConfigureOptions<UserDefaultSettings>
    {
        #region Private variables
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructors
        public UserDefaultSettingsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Public methods
        public void Configure(UserDefaultSettings userDefaultSettings)
        {
            _configuration
                .GetSection("UserDefaultSettings")
                .Bind(userDefaultSettings);
        }
        #endregion
    }
}

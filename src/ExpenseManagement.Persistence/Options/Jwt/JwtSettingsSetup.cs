namespace ExpenseManagement.Persistence.Options.Jwt
{
    public sealed class JwtSettingsSetup : IConfigureOptions<JwtSettings>
    {
        private readonly IConfiguration _configuration;

        public JwtSettingsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(JwtSettings jwtSettings)
        {
            _configuration
                .GetSection("Jwt")
                .Bind(jwtSettings);
        }
    }
}

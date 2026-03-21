namespace ExpenseManagement.Application.Options.JwtBearer
{
    public sealed class JwtBearerOptionsSetup : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly JwtSettings _jwtSettings;

        public JwtBearerOptionsSetup(IOptions<JwtSettings> options)
        {
            _jwtSettings = options.Value;
        }

        public void Configure(JwtBearerOptions options)
        {
            Configure(null, options);
        }

        public void Configure(string? name, JwtBearerOptions options)
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
                RoleClaimType = ClaimTypes.Role,
                //ClockSkew = TimeSpan.Zero
            };

            options.Events = new JwtBearerEvents()
            {
                OnAuthenticationFailed = context =>
                {
                    Console.WriteLine($"Erro: {context.Exception.Message}");
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    Console.WriteLine("Token válido!");
                    return Task.CompletedTask;
                },
                OnChallenge = context =>
                {
                    Console.WriteLine("Challenge triggered");
                    return Task.CompletedTask;
                },

                OnMessageReceived = context =>
                {
                    Console.WriteLine($"TOKEN RECEIVED: {context.Request.Headers["Authorization"]}");
                    return Task.CompletedTask;
                }
            };
        }
    }
}

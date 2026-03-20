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
            RSA rsa = RSA.Create();

            rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(_jwtSettings.PubKey), out _);

            options.MapInboundClaims = false;

            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new RsaSecurityKey(rsa),
                RoleClaimType = ClaimTypes.Role
            };

            options.Events = new JwtBearerEvents()
            {
                OnAuthenticationFailed = context =>
                {
                    Console.WriteLine("ERRROROR");
                    return Task.CompletedTask;
                }
            };
        }
    }
}

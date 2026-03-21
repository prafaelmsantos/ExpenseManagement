namespace ExpenseManagement.Application.Services
{
    public sealed class TokenService : ITokenService
    {
        #region Private variables
        private readonly JwtSettings _jwtSettings;
        #endregion

        #region Constructor
        public TokenService(IOptions<JwtSettings> options)
        {
            _jwtSettings = options.Value;
        }
        #endregion

        #region Public methods
        public UserTokenDTO GenerateToken(List<Claim> claims)
        {
            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new UserTokenDTO()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
        #endregion
    }
}

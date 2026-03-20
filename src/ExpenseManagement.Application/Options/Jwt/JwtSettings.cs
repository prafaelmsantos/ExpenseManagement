namespace ExpenseManagement.Application.Options.Jwt
{
    public sealed record JwtSettings
    {
        public required string PubKey { get; init; }
        public required string Issuer { get; init; }
        public required string Audience { get; init; }
    }
}

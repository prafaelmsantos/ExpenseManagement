namespace ExpenseManagement.Application.Options.Jwt
{
    internal sealed class JwtSettingsValidator : IValidateOptions<JwtSettings>
    {
        public ValidateOptionsResult Validate(string? name, JwtSettings jwtSettings)
        {
            List<string> errors = [];

            if (jwtSettings is null)
            {
                return ValidateOptionsResult.Fail($"{nameof(JwtSettings)} must be provided");
            }

            if (string.IsNullOrWhiteSpace(jwtSettings.Key))
            {
                errors.Add($"{nameof(JwtSettings)}.{nameof(JwtSettings.Key)} must have a value");
            }

            if (string.IsNullOrWhiteSpace(jwtSettings.Issuer))
            {
                errors.Add($"{nameof(JwtSettings)}.{nameof(JwtSettings.Issuer)} must have a value");
            }

            if (string.IsNullOrWhiteSpace(jwtSettings.Audience))
            {
                errors.Add($"{nameof(JwtSettings)}.{nameof(JwtSettings.Audience)} must have a value");
            }

            return errors.Any()
                ? ValidateOptionsResult.Fail(errors)
                : ValidateOptionsResult.Success;
        }
    }
}

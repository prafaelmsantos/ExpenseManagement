namespace ExpenseManagement.API.Core.Swagger.Options
{
    internal sealed class SwaggerSettingsValidator : IValidateOptions<SwaggerSettings>
    {
        public ValidateOptionsResult Validate(string? name, SwaggerSettings swaggerSettings)
        {
            List<string> errors = [];

            if (swaggerSettings is null)
            {
                return ValidateOptionsResult.Fail($"{nameof(SwaggerSettings)} must be provided");
            }

            if (string.IsNullOrWhiteSpace(swaggerSettings.Title))
            {
                errors.Add($"{nameof(SwaggerSettings)}.{nameof(SwaggerSettings.Title)} must have a value");
            }

            return errors.Any()
                ? ValidateOptionsResult.Fail(errors)
                : ValidateOptionsResult.Success;
        }
    }
}

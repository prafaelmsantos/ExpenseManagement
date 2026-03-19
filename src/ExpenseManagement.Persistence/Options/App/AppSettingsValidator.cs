namespace ExpenseManagement.Persistence.Options.App
{
    internal sealed class AppSettingsValidator : IValidateOptions<AppSettings>
    {
        public ValidateOptionsResult Validate(string? _, AppSettings appSettings)
        {
            List<string> errors = [];

            if (appSettings is null)
            {
                return ValidateOptionsResult.Fail($"{nameof(AppSettings)} must be provided");
            }

            if (string.IsNullOrWhiteSpace(appSettings.BasePath))
            {
                errors.Add($"{nameof(AppSettings)}.{nameof(AppSettings.BasePath)} must have a value");
            }

            if (appSettings.ConnectionStrings is null)
            {
                return ValidateOptionsResult.Fail($"{nameof(ConnectionString)} must be provided");
            }

            if (string.IsNullOrWhiteSpace(appSettings.ConnectionStrings.Db))
            {
                errors.Add($"{nameof(AppSettings)}.{nameof(AppSettings.ConnectionStrings.Db)} must have a value");
            }

            return errors.Count != 0
                ? ValidateOptionsResult.Fail(errors)
                : ValidateOptionsResult.Success;
        }
    }
}

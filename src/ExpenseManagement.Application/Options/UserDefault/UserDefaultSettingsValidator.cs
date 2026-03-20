namespace ExpenseManagement.Application.Options.UserDefault
{
    internal sealed class UserDefaultSettingsValidator : IValidateOptions<UserDefaultSettings>
    {
        public ValidateOptionsResult Validate(string? _, UserDefaultSettings userDefaultSettings)
        {
            List<string> errors = [];

            if (userDefaultSettings is null)
            {
                return ValidateOptionsResult.Fail($"{nameof(UserDefaultSettings)} must be provided");
            }

            if (string.IsNullOrWhiteSpace(userDefaultSettings.UserName))
            {
                errors.Add($"{nameof(UserDefaultSettings)}.{nameof(UserDefaultSettings.UserName)} must have a value");
            }

            if (string.IsNullOrWhiteSpace(userDefaultSettings.Password))
            {
                errors.Add($"{nameof(UserDefaultSettings)}.{nameof(UserDefaultSettings.Password)} must have a value");
            }

            if (string.IsNullOrWhiteSpace(userDefaultSettings.RoleAdmin))
            {
                errors.Add($"{nameof(UserDefaultSettings)}.{nameof(UserDefaultSettings.RoleAdmin)} must have a value");
            }

            if (string.IsNullOrWhiteSpace(userDefaultSettings.Role))
            {
                errors.Add($"{nameof(UserDefaultSettings)}.{nameof(UserDefaultSettings.Role)} must have a value");
            }

            return errors.Count != 0
                ? ValidateOptionsResult.Fail(errors)
                : ValidateOptionsResult.Success;
        }
    }
}

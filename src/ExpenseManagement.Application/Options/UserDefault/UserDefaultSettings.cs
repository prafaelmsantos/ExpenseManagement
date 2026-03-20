namespace ExpenseManagement.Application.Options.UserDefault
{
    public sealed class UserDefaultSettings
    {
        public string UserName { get; init; } = null!;
        public string Password { get; init; } = null!;
        public string RoleAdmin { get; init; } = null!;
        public string Role { get; init; } = null!;
    }
}

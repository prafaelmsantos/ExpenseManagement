namespace ExpenseManagement.Domain.Entities.Identity
{
    public class User : IdentityUser<Guid>
    {
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public bool DarkMode { get; private set; }
        public bool IsDefault { get; private set; }

        public virtual ICollection<UserClaim> Claims { get; private set; } = [];
        public virtual ICollection<UserLogin> Logins { get; private set; } = [];
        public virtual ICollection<UserToken> Tokens { get; private set; } = [];
        public virtual ICollection<UserRole> UserRoles { get; private set; } = [];

        public User() { }

        public User(string email, bool isDefault = false)
        {
            Email = email;
            UserName = email;
            IsDefault = isDefault;
        }

        public User(string? firstName, string? lastName, string email, bool isDefault = false)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserName = email;
            IsDefault = isDefault;
        }
    }
}

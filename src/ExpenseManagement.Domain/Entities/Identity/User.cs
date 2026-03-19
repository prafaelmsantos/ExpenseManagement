namespace ExpenseManagement.Domain.Entities.Identity
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;
        public bool DarkMode { get; private set; }
        public bool IsDefault { get; private set; }

        public virtual ICollection<UserClaim> Claims { get; private set; } = [];
        public virtual ICollection<UserLogin> Logins { get; private set; } = [];
        public virtual ICollection<UserToken> Tokens { get; private set; } = [];
        public virtual ICollection<UserRole> UserRoles { get; private set; } = [];

        public User() { }
    }
}

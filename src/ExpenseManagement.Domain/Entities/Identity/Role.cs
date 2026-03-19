namespace ExpenseManagement.Domain.Entities.Identity
{
    public class Role : IdentityRole<Guid>
    {
        public bool IsDefault { get; private set; }

        public virtual ICollection<RoleClaim> RoleClaims { get; private set; } = [];
        public virtual ICollection<UserRole> UserRoles { get; private set; } = [];

        public Role() { }
    }
}

namespace ExpenseManagement.Domain.Entities.Identity
{
    public class Role : IdentityRole<Guid>
    {
        public virtual ICollection<RoleClaim> RoleClaims { get; private set; } = [];
        public virtual ICollection<UserRole> UserRoles { get; private set; } = [];

        public Role() { }

        public Role(string name)
        {
            Name = name;
        }
    }
}

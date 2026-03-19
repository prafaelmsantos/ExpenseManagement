namespace ExpenseManagement.Domain.Entities.Identity
{
    public class RoleClaim : IdentityRoleClaim<Guid>
    {
        public virtual Role Role { get; private set; } = null!;

        public RoleClaim() { }
    }
}

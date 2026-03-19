namespace ExpenseManagement.Domain.Entities.Identity
{
    public class UserClaim : IdentityUserClaim<Guid>
    {
        public virtual User User { get; private set; } = null!;

        public UserClaim() { }
    }
}

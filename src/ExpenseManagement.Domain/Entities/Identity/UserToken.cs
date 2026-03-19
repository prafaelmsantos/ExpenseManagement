namespace ExpenseManagement.Domain.Entities.Identity
{
    public class UserToken : IdentityUserToken<Guid>
    {
        public virtual User User { get; private set; } = null!;

        public UserToken() { }
    }
}

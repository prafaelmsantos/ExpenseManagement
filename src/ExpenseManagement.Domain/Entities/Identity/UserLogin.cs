namespace ExpenseManagement.Domain.Entities.Identity
{
    public class UserLogin : IdentityUserLogin<Guid>
    {
        public virtual User User { get; private set; } = null!;

        public UserLogin() { }
    }
}

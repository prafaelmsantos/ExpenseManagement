namespace ExpenseManagement.Domain.Entities.Identity
{
    public class User : IdentityUser<Guid>
    {
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }

        public virtual ICollection<UserClaim> Claims { get; private set; } = [];
        public virtual ICollection<UserLogin> Logins { get; private set; } = [];
        public virtual ICollection<UserToken> Tokens { get; private set; } = [];
        public virtual ICollection<UserRole> UserRoles { get; private set; } = [];
        public virtual ICollection<Expense> Expenses { get; private set; } = [];
        public virtual ICollection<Saving> Savings { get; private set; } = [];

        public User() { }

        public User(string userName)
        {
            Validator.New()
                .When(string.IsNullOrWhiteSpace(userName), "O userName do utilizador é invalido.")
                .TriggerBadRequestExceptionIfExist();

            Email = userName;
            UserName = userName;
        }

        public User(string userName, string? firstName, string? lastName)
        {
            Validator.New()
               .When(string.IsNullOrWhiteSpace(userName), "O nome de utilizador é invalido.")
               .TriggerBadRequestExceptionIfExist();

            Email = userName;
            UserName = userName;

            FirstName = firstName;
            LastName = lastName;
        }

        public void Update(string? firstName, string? lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public void Update(string userName, string? firstName, string? lastName)
        {
            Validator.New()
                .When(string.IsNullOrWhiteSpace(userName), "O nome de utilizador é invalido.")
                .TriggerBadRequestExceptionIfExist();

            FirstName = firstName;
            LastName = lastName;
        }
    }
}

namespace ExpenseManagement.Domain.Entities.Identity
{
    public class Role : IdentityRole<Guid>
    {
        public virtual ICollection<RoleClaim> RoleClaims { get; private set; } = [];
        public virtual ICollection<UserRole> UserRoles { get; private set; } = [];

        public Role() { }

        public Role(string name)
        {
            Validator.New()
                .When(string.IsNullOrWhiteSpace(name), "O nome do cargo é invalido.")
                .TriggerBadRequestExceptionIfExist();

            Name = name;
        }
    }
}

namespace ExpenseManagement.Domain.Entities
{
    public class Expense : BaseEntity
    {
        public Guid UserId { get; private set; }
        public virtual User User { get; private set; } = null!;
        public string Name { get; private set; } = null!;
        public decimal Amount { get; private set; }
        public Category Category { get; private set; }
        public DateTime Date { get; private set; }
        public string? Description { get; private set; }

        protected Expense() { }

        public Expense(Guid userId, string name, decimal amount, Category category, DateTime date, string? description)
        {
            Validator.New()
               .When(string.IsNullOrWhiteSpace(name), "O nome da despesa é invalido.")
               .When(amount <= 0, "A quantia da despesa é invalida.")
               .TriggerBadRequestExceptionIfExist();

            UserId = userId;
            Name = name;
            Amount = amount;
            Category = category;
            Date = date;
            Description = description;
        }

        public void Update(string name, decimal amount, Category category, DateTime date, string? description)
        {
            Validator.New()
               .When(string.IsNullOrWhiteSpace(name), "O nome da despesa é invalido.")
               .When(amount <= 0, "A quantia da despesa é invalida.")
               .TriggerBadRequestExceptionIfExist();

            Name = name;
            Amount = amount;
            Category = category;
            Date = date;
            Description = description;
        }
    }
}

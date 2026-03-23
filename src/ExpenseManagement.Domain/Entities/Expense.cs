namespace ExpenseManagement.Domain.Entities
{
    public class Expense : BaseEntity
    {
        public Guid UserId { get; private set; }
        public virtual User User { get; private set; } = null!;
        public decimal Amount { get; private set; }
        public ExpenseCategory Category { get; private set; }
        public DateTime Date { get; private set; }

        protected Expense() { }

        public Expense(Guid userId, decimal amount, ExpenseCategory category, DateTime date)
        {
            UserId = userId;
            Amount = amount;
            Category = category;
            Date = date;
        }

        public void Update(decimal amount, ExpenseCategory category, DateTime date)
        {
            Amount = amount;
            Category = category;
            Date = date;
        }
    }
}

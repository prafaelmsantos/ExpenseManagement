namespace ExpenseManagement.Domain.Entities
{
    public class Saving : BaseEntity
    {
        public Guid UserId { get; private set; }
        public virtual User User { get; private set; } = null!;
        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }

        protected Saving() { }

        public Saving(Guid userId, decimal amount, DateTime date)
        {
            UserId = userId;
            Amount = amount;
            Date = date;
        }

        public void Update(decimal amount, DateTime date)
        {
            Amount = amount;
            Date = date;
        }
    }
}

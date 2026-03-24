namespace ExpenseManagement.Domain.Entities
{
    public class Saving : BaseEntity
    {
        public Guid UserId { get; private set; }
        public string Name { get; private set; } = null!;
        public virtual User User { get; private set; } = null!;
        public decimal Amount { get; private set; }
        public Category Category { get; private set; }
        public DateTime Date { get; private set; }
        public string? Description { get; private set; }

        protected Saving() { }

        public Saving(Guid userId, string name, decimal amount, Category category, DateTime date, string? description)
        {
            UserId = userId;
            Name = name;
            Amount = amount;
            Category = category;
            Date = date;
            Description = description;
        }

        public void Update(string name, decimal amount, Category category, DateTime date, string? description)
        {
            Name = name;
            Amount = amount;
            Category = category;
            Date = date;
            Description = description;
        }
    }
}

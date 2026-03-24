namespace ExpenseManagement.Application.Dtos
{
    public sealed class ExpenseDTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Amount { get; set; }
        public Category Category { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
    }
}

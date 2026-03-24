namespace ExpenseManagement.Application.Dtos
{
    public sealed class ExpenseTableDTO
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public Category Category { get; set; }
        public string Date { get; set; } = null!;
    }
}

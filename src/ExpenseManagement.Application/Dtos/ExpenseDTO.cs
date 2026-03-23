namespace ExpenseManagement.Application.Dtos
{
    public sealed class ExpenseDTO
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public ExpenseCategory Category { get; set; }
        public DateTime Date { get; set; }
    }
}

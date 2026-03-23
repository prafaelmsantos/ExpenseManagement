namespace ExpenseManagement.Application.Dtos
{
    public sealed class SavingDTO
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}

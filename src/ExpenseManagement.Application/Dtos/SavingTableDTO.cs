namespace ExpenseManagement.Application.Dtos
{
    public sealed class SavingTableDTO
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Date { get; set; } = null!;
    }
}

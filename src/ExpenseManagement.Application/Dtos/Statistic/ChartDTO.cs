namespace ExpenseManagement.Application.Dtos.Statistic
{
    public sealed class ChartDTO
    {
        public string Name { get; set; } = null!;
        public List<decimal> Data { get; set; } = [];
        public decimal AmountTotal => Math.Round(Data.Sum(), 2);
    }
}

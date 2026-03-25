namespace ExpenseManagement.Application.Dtos.Statistic
{
    public sealed class BarChartDTO
    {
        public string Id { get; set; } = null!;
        public string Label { get; set; } = null!;
        public List<decimal> Data { get; set; } = [];
        public decimal AmountTotal => Data.Sum();
    }
}

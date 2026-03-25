namespace ExpenseManagement.Application.Dtos.Statistic
{
    public sealed class SparkLineChartDTO
    {
        public string Name { get; set; } = null!;
        public decimal Amount => Math.Round(Data.Sum(), 2);
        public int Interval => Data.Count;
        public string Trend { get; set; } = null!;
        public List<decimal> Data { get; set; } = [];
    }
}

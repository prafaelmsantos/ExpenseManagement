namespace ExpenseManagement.Application.Dtos
{
    public sealed class StatCardDTO
    {
        public string Title { get; set; } = null!;
        public decimal Value { get; set; }
        public string Interval { get; set; } = null!;
        public string Trend { get; set; } = null!;
        public List<decimal> Data { get; set; } = [];
        public List<string> Labels { get; set; } = [];
    }
}

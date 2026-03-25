namespace ExpenseManagement.Application.Dtos
{
    public sealed class LineSeriesDTO
    {
        public string Name { get; set; } = null!;
        public decimal AmountTotal { get; set; }
        public List<decimal> Data { get; set; } = [];
    }
}

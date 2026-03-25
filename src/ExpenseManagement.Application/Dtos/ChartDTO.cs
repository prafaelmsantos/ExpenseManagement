namespace ExpenseManagement.Application.Dtos
{
    public sealed class ChartDTO
    {
        public List<string> Labels { get; set; } = [];
        public List<LineSeriesDTO> Series { get; set; } = [];
    }
}

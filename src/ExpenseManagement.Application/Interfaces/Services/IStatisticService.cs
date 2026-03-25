namespace ExpenseManagement.Application.Interfaces.Services
{
    public interface IStatisticService
    {
        Task<List<SparkLineChartDTO>> GetSparkLineChartAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<YearDTO> GetYearsAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<List<ChartDTO>> GetExpensesLineChartAsync(Guid userId, List<int> years, CancellationToken cancellationToken = default);
        Task<List<ChartDTO>> GetSavingsLineChartAsync(Guid userId, List<int> years, CancellationToken cancellationToken = default);
        Task<List<ChartDTO>> GetExpensesBarChartAsync(Guid userId, List<int> years, CancellationToken cancellationToken = default);
        Task<List<ChartDTO>> GetSavingsBarChartAsync(Guid userId, List<int> years, CancellationToken cancellationToken = default);
    }
}

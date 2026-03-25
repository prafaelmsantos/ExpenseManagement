namespace ExpenseManagement.Application.Interfaces.Services
{
    public interface IStatisticService
    {
        Task<YearDTO> GetYearsAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<ChartDTO> GetExpensesByYearsAsync(Guid userId, List<int> years, CancellationToken cancellationToken = default);
        Task<ChartDTO> GetSavingsByYearsAsync(Guid userId, List<int> years, CancellationToken cancellationToken = default);
    }
}

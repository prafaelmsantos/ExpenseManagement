using System.Globalization;

namespace ExpenseManagement.Application.Services
{
    public sealed class StatisticService : IStatisticService
    {
        #region Private variables
        private readonly IExpenseRepository _expenseRepository;
        private readonly ISavingRepository _savingRepository;
        #endregion

        #region Constructor
        public StatisticService(IExpenseRepository expenseRepository, ISavingRepository savingRepository)
        {
            _expenseRepository = expenseRepository;
            _savingRepository = savingRepository;
        }
        #endregion

        #region Public methods

        public async Task<YearDTO> GetYearsAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var expenseYears = await _expenseRepository
                .GetAllQueryable()
                .Where(x => x.UserId == userId)
                .Select(x => x.Date.Year)
                .Distinct()
                .ToListAsync(cancellationToken);

            var savingYears = await _savingRepository
                .GetAllQueryable()
                .Where(x => x.UserId == userId)
                .Select(x => x.Date.Year)
                .Distinct()
                .ToListAsync(cancellationToken);

            var years = expenseYears
                .Concat(savingYears)
                .Append(DateTime.UtcNow.Year)
                .Distinct()
                .OrderByDescending(y => y)
                .ToList();

            return new YearDTO() { Years = years };
        }

        public async Task<List<StatCardDTO>> GetStatisticsByCurrentMonthAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            DateTime today = DateTime.UtcNow.Date;
            DateTime startDate = new(today.Year, today.Month, 1);
            int totalDays = (today - startDate).Days + 1;

            List<DateTime> days = [.. Enumerable.Range(0, totalDays).Select(i => startDate.AddDays(i))];

            Dictionary<DateTime, decimal> expenses = await GetExpensesByMonthAsync(today, startDate, userId, cancellationToken);
            Dictionary<DateTime, decimal> savings = await GetSavingsByMonthAsync(today, startDate, userId, cancellationToken);

            List<StatCardDTO> stats = [GetStats(expenses, days), GetStats(savings, days)];

            return stats;
        }

        public async Task<ChartDTO> GetExpensesByYearsAsync(Guid userId, List<int> years, CancellationToken cancellationToken = default)
        {
            List<DateTime> months = [.. Enumerable.Range(0, 12)
                .Select(i => new DateTime(DateTime.UtcNow.Year, 1, 1)
                .AddMonths(i))];

            List<LineSeriesDTO> lineSeries = [];
            foreach (var year in years)
            {
                DateTime startDate = new(year, 1, 1);
                DateTime endDate = new(year, 12, 31);

                Dictionary<DateTime, decimal> expenses = await GetExpensesByYearAsync(userId, startDate, endDate, cancellationToken);

                lineSeries.Add(GetStats(expenses, year));
            }

            return new ChartDTO()
            {
                Labels = [.. months.Select(m => m.ToString("MMM", new CultureInfo("pt-PT")))],
                Series = lineSeries
            };
        }

        public async Task<ChartDTO> GetSavingsByYearsAsync(Guid userId, List<int> years, CancellationToken cancellationToken = default)
        {
            List<DateTime> months = [.. Enumerable.Range(0, 12)
                .Select(i => new DateTime(DateTime.UtcNow.Year, 1, 1)
                .AddMonths(i))];

            List<LineSeriesDTO> lineSeries = [];
            foreach (var year in years)
            {
                DateTime startDate = new(year, 1, 1);
                DateTime endDate = new(year, 12, 31);

                Dictionary<DateTime, decimal> expenses = await GetSavingsByYearAsync(userId, startDate, endDate, cancellationToken);

                lineSeries.Add(GetStats(expenses, year));
            }

            return new ChartDTO()
            {
                Labels = [.. months.Select(m => m.ToString("MMM", new CultureInfo("pt-PT")))],
                Series = lineSeries
            };
        }
        #endregion

        #region Private methods
        private async Task<Dictionary<DateTime, decimal>> GetExpensesByMonthAsync(
            DateTime today,
            DateTime startDate,
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            var expenses = await _expenseRepository
                .GetAllQueryable()
                .Where(x => x.Date >= startDate && x.Date <= today && x.UserId == userId)
                .ToListAsync(cancellationToken);

            Dictionary<DateTime, decimal> grouped = expenses
                .GroupBy(e => e.Date.Date)
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(e => e.Amount)
                );

            return grouped;
        }

        private async Task<Dictionary<DateTime, decimal>> GetSavingsByMonthAsync(
            DateTime today,
            DateTime startDate,
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            var savings = await _savingRepository
                .GetAllQueryable()
                .Where(x => x.Date >= startDate && x.Date <= today && x.UserId == userId)
                .ToListAsync(cancellationToken);

            Dictionary<DateTime, decimal> grouped = savings
                .GroupBy(e => e.Date.Date)
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(e => e.Amount)
                );

            return grouped;
        }

        private static StatCardDTO GetStats(Dictionary<DateTime, decimal> grouped, List<DateTime> days)
        {
            var data = days
                .Select(day => grouped.TryGetValue(day, out decimal value) ? value : 0)
                .ToList();

            var labels = days
                .Select(day => day.ToString("dd MMM", new CultureInfo("pt-PT")))
                .ToList();

            int half = data.Count / 2;

            var firstHalf = data.Take(half).Sum();
            var secondHalf = data.Skip(half).Sum();

            var trend = secondHalf == firstHalf
                ? "neutral"
                : secondHalf > firstHalf ? "up" : "down";

            var statCardDTO = new StatCardDTO()
            {
                Title = "Despesas",
                Value = data.Sum(),
                Interval = "Mês atual",
                Trend = trend,
                Data = data,
                Labels = labels
            };

            return statCardDTO;
        }

        private async Task<Dictionary<DateTime, decimal>> GetExpensesByYearAsync(
            Guid userId,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken = default)
        {
            var expenses = await _expenseRepository
                .GetAllQueryable()
                .Where(x => x.Date >= startDate && x.Date <= endDate && x.UserId == userId)
                .ToListAsync(cancellationToken);

            return expenses
                .GroupBy(e => new { e.Date.Year, e.Date.Month })
                .ToDictionary(
                    g => new DateTime(g.Key.Year, g.Key.Month, 1),
                    g => g.Sum(e => e.Amount)
                );
        }

        private async Task<Dictionary<DateTime, decimal>> GetSavingsByYearAsync(
            Guid userId,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken = default)
        {
            var savings = await _savingRepository
                .GetAllQueryable()
                .Where(x => x.Date >= startDate && x.Date <= endDate && x.UserId == userId)
                .ToListAsync(cancellationToken);

            return savings
                .GroupBy(e => new { e.Date.Year, e.Date.Month })
                .ToDictionary(
                    g => new DateTime(g.Key.Year, g.Key.Month, 1),
                    g => g.Sum(e => e.Amount)
                );
        }

        private static LineSeriesDTO GetStats(
            Dictionary<DateTime, decimal> grouped,
            int year)
        {
            List<DateTime> months = [.. Enumerable.Range(0, 12)
                .Select(i => new DateTime(year, 1, 1)
                .AddMonths(i))];

            var data = months
                .Select(month => grouped.TryGetValue(month, out decimal value) ? value : 0)
                .ToList();

            return new LineSeriesDTO()
            {
                Name = year.ToString(),
                AmountTotal = Math.Round(data.Sum(), 2),
                Data = data
            };
        }
        #endregion
    }
}

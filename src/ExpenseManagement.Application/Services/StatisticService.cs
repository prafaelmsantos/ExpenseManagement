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

        public async Task<List<SparkLineChartDTO>> GetSparkLineChartAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            DateTime today = DateTime.UtcNow.Date;
            DateTime startDate = new(today.Year, today.Month, 1);
            int totalDays = (today - startDate).Days + 1;

            List<DateTime> days = [.. Enumerable.Range(0, totalDays).Select(i => startDate.AddDays(i))];

            Dictionary<DateTime, decimal> expenses = await GetExpensesByMonthAsync(today, startDate, userId, cancellationToken);
            Dictionary<DateTime, decimal> savings = await GetSavingsByMonthAsync(today, startDate, userId, cancellationToken);

            List<SparkLineChartDTO> stats = [
                GetStats(savings, days, isSavings: true),
                GetStats(expenses, days, isSavings: false)
            ];

            return stats;
        }

        public async Task<List<ChartDTO>> GetExpensesLineChartAsync(Guid userId, List<int> years, CancellationToken cancellationToken = default)
        {
            List<ChartDTO> charts = [];

            foreach (var year in years)
            {
                DateTime startDate = new(year, 1, 1);
                DateTime endDate = new(year, 12, 31);

                Dictionary<DateTime, decimal> expenses = await GetExpensesByYearAsync(userId, startDate, endDate, cancellationToken);

                charts.Add(GetLineChart(expenses, year));
            }

            return charts;
        }

        public async Task<List<ChartDTO>> GetSavingsLineChartAsync(Guid userId, List<int> years, CancellationToken cancellationToken = default)
        {
            List<ChartDTO> charts = [];

            foreach (var year in years)
            {
                DateTime startDate = new(year, 1, 1);
                DateTime endDate = new(year, 12, 31);

                Dictionary<DateTime, decimal> expenses = await GetSavingsByYearAsync(userId, startDate, endDate, cancellationToken);

                charts.Add(GetLineChart(expenses, year));
            }

            return charts;
        }

        public async Task<List<ChartDTO>> GetExpensesBarChartAsync(Guid userId, List<int> years, CancellationToken cancellationToken = default)
        {
            List<ChartDTO> charts = [];

            foreach (var year in years)
            {
                ChartDTO lineChartDTO = await GetExpensesByCategoryAndYearAsync(userId, year, cancellationToken);

                charts.Add(lineChartDTO);
            }

            return charts;
        }

        public async Task<List<ChartDTO>> GetSavingsBarChartAsync(Guid userId, List<int> years, CancellationToken cancellationToken = default)
        {
            List<ChartDTO> charts = [];

            foreach (var year in years)
            {
                ChartDTO lineChartDTO = await GetSavingsByCategoryAndYearAsync(userId, year, cancellationToken);

                charts.Add(lineChartDTO);
            }

            return charts;
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

        private static SparkLineChartDTO GetStats(Dictionary<DateTime, decimal> grouped, List<DateTime> days, bool isSavings)
        {
            List<decimal> data = [.. days.Select(day => grouped.TryGetValue(day, out decimal value) ? value : 0)];

            SparkLineChartDTO statCardDTO = new()
            {
                Name = isSavings ? "Poupanças" : "Despesas",
                Trend = GetTrend(data, isSavings),
                Data = data,
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

        private static ChartDTO GetLineChart(
            Dictionary<DateTime, decimal> grouped,
            int year)
        {
            List<DateTime> months = [.. Enumerable.Range(0, 12)
                .Select(i => new DateTime(year, 1, 1)
                .AddMonths(i))];

            var data = months
                .Select(month => grouped.TryGetValue(month, out decimal value) ? value : 0)
                .ToList();

            return new ChartDTO()
            {
                Name = year.ToString(),
                Data = data
            };
        }

        private async Task<ChartDTO> GetExpensesByCategoryAndYearAsync(
            Guid userId,
            int year,
            CancellationToken cancellationToken = default)
        {
            var expenses = await _expenseRepository
                .GetAllQueryable()
                .Where(x => x.Date.Year == year && x.UserId == userId)
                .ToListAsync(cancellationToken);

            List<decimal> data = [.. Enum.GetValues<Category>()
                .Select(cat => expenses
                .Where(e => e.Category == cat)
                .Sum(e => e.Amount))];

            return new ChartDTO()
            {
                Name = year.ToString(),
                Data = data
            };
        }

        private async Task<ChartDTO> GetSavingsByCategoryAndYearAsync(
            Guid userId,
            int year,
            CancellationToken cancellationToken = default)
        {
            var savings = await _savingRepository
                .GetAllQueryable()
                .Where(x => x.Date.Year == year && x.UserId == userId)
                .ToListAsync(cancellationToken);

            List<decimal> data = [.. Enum.GetValues<Category>()
                .Select(cat => savings
                .Where(e => e.Category == cat)
                .Sum(e => e.Amount))];

            return new ChartDTO()
            {
                Name = year.ToString(),
                Data = data
            };
        }

        private static string GetTrend(List<decimal> data, bool isSavings)
        {
            int half = data.Count / 2;

            decimal firstHalf = data.Take(half).Sum();
            decimal secondHalf = data.Skip(half).Sum();

            if (secondHalf == firstHalf)
            {
                return "neutral";
            }


            bool isUp = secondHalf > firstHalf;
            return isSavings
                ? (isUp ? "up" : "down")
                : (isUp ? "down" : "up");
        }

        #endregion
    }
}

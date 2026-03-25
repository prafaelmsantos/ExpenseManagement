namespace ExpenseManagement.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        #region Private variables
        private readonly IStatisticService _statisticService;
        #endregion

        #region Constructor
        public StatisticsController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }
        #endregion

        #region CRUD Methods

        /// <summary> Get Years </summary>
        [HttpGet("years")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetYearsAsync()
        {
            Guid userId = HttpContext.User.GetUserId();

            YearDTO yearDTO = await _statisticService.GetYearsAsync(userId);

            return Ok(yearDTO);
        }

        /// <summary> Get Spark Line Chart </summary>
        [HttpGet("chart/spark/line")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetSparkLineChartAsync()
        {
            Guid userId = HttpContext.User.GetUserId();

            List<SparkLineChartDTO> sparkLineCharts = await _statisticService.GetSparkLineChartAsync(userId);

            return Ok(sparkLineCharts);
        }

        /// <summary> Get Expenses Line Chart </summary>
        /// <param name="yearDTO"></param>
        [HttpPost("expenses/chart/line")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetExpensesLineChartAsync([FromBody] YearDTO yearDTO)
        {
            Guid userId = HttpContext.User.GetUserId();

            List<ChartDTO> charts = await _statisticService.GetExpensesLineChartAsync(userId, yearDTO.Years);

            return Ok(charts);
        }

        /// <summary> Get Savings Line Chart </summary>
        /// <param name="yearDTO"></param>
        [HttpPost("savings/chart/line")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetSavingsLineChartAsync([FromBody] YearDTO yearDTO)
        {
            Guid userId = HttpContext.User.GetUserId();

            List<ChartDTO> charts = await _statisticService.GetSavingsLineChartAsync(userId, yearDTO.Years);

            return Ok(charts);
        }

        /// <summary> Get Expenses Bar Chart </summary>
        /// <param name="yearDTO"></param>
        [HttpPost("expenses/chart/bar")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetExpensesBarChartAsync([FromBody] YearDTO yearDTO)
        {
            Guid userId = HttpContext.User.GetUserId();

            List<ChartDTO> charts = await _statisticService.GetExpensesBarChartAsync(userId, yearDTO.Years);

            return Ok(charts);
        }

        /// <summary> Get Savings Bar Chart </summary>
        /// <param name="yearDTO"></param>
        [HttpPost("savings/chart/bar")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetSavingsBarChartAsync([FromBody] YearDTO yearDTO)
        {
            Guid userId = HttpContext.User.GetUserId();

            List<ChartDTO> charts = await _statisticService.GetSavingsBarChartAsync(userId, yearDTO.Years);

            return Ok(charts);
        }
        #endregion
    }
}

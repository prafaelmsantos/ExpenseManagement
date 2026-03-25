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
        public async Task<IActionResult> GetExpensesByYearsAsync()
        {
            Guid userId = HttpContext.User.GetUserId();

            YearDTO yearResponseDTO = await _statisticService.GetYearsAsync(userId);

            return Ok(yearResponseDTO);
        }

        /// <summary> Get All Expenses </summary>
        /// <param name="yearDTO"></param>
        [HttpPost("expenses/years")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetExpensesByYearsAsync([FromBody] YearDTO yearDTO)
        {
            Guid userId = HttpContext.User.GetUserId();

            ChartDTO lineChartDTO = await _statisticService.GetExpensesByYearsAsync(userId, yearDTO.Years);

            return Ok(lineChartDTO);
        }

        /// <summary> Get All Savings </summary>
        /// <param name="yearDTO"></param>
        [HttpPost("savings/years")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetSavingsByYearsAsync([FromBody] YearDTO yearDTO)
        {
            Guid userId = HttpContext.User.GetUserId();

            ChartDTO lineChartDTO = await _statisticService.GetSavingsByYearsAsync(userId, yearDTO.Years);

            return Ok(lineChartDTO);
        }
        #endregion
    }
}

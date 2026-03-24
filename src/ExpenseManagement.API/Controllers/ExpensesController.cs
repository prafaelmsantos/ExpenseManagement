namespace ExpenseManagement.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        #region Private variables
        private readonly IExpenseService _expenseService;
        #endregion

        #region Constructor
        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }
        #endregion

        #region CRUD Methods

        /// <summary> Get All Expenses </summary>
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllExpensesAsync()
        {
            Guid userId = HttpContext.User.GetUserId();

            List<ExpenseDTO> expenses = await _expenseService.GetAllExpensesAsync(userId);
            return Ok(expenses);
        }

        /// <summary> Get Expense By Id </summary>
        /// <param name="expenseId"></param>
        [HttpGet("{expenseId}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetExpenseByIdAsync([FromRoute] Guid expenseId)
        {
            Guid userId = HttpContext.User.GetUserId();

            Validator.New()
                .When(expenseId == default, "O Id da despesa é invalido.")
                .TriggerBadRequestExceptionIfExist();

            ExpenseDTO expenseDTO = await _expenseService.GetExpenseByIdAsync(expenseId, userId);

            return Ok(expenseDTO);
        }

        /// <summary> Create Expense </summary>
        /// <param name="expenseDTO"></param>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> AddExpenseAsync([FromBody] ExpenseDTO expenseDTO)
        {
            Guid userId = HttpContext.User.GetUserId();

            expenseDTO = await _expenseService.AddExpenseAsync(expenseDTO, userId);
            return Ok(expenseDTO);
        }

        /// <summary> Update Expense </summary>
        /// <param name="expenseId"></param>
        /// <param name="expenseDTO"></param>
        [HttpPut("{expenseId}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateExpenseAsync([FromRoute] Guid expenseId, [FromBody] ExpenseDTO expenseDTO)
        {
            Guid userId = HttpContext.User.GetUserId();

            Validator.New()
                .When(expenseId == default, "O Id da despesa é invalido.")
                .TriggerBadRequestExceptionIfExist();

            expenseDTO.Id = expenseId;

            expenseDTO = await _expenseService.UpdateExpenseAsync(expenseDTO, userId);
            return Ok(expenseDTO);
        }

        //// <summary> Delete Expenses  </summary>
        /// <param name="expenseIds"></param>
        [HttpPost("delete")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteExpensesAsync([FromBody] List<Guid> expenseIds)
        {
            Guid userId = HttpContext.User.GetUserId();

            List<BaseResponseDTO> baseResponses = await _expenseService.DeleteExpensesAsync(expenseIds, userId);

            return Ok(baseResponses);
        }
        #endregion
    }
}

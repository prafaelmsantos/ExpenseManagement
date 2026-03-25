namespace ExpenseManagement.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SavingsController : ControllerBase
    {
        #region Private variables
        private readonly ISavingService _savingService;
        #endregion

        #region Constructor
        public SavingsController(ISavingService savingService)
        {
            _savingService = savingService;
        }
        #endregion

        #region CRUD Methods

        /// <summary> Get All Savings </summary>
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllSavingsAsync()
        {
            Guid userId = HttpContext.User.GetUserId();

            List<SavingDTO> savings = await _savingService.GetAllSavingsAsync(userId);
            return Ok(savings);
        }

        /// <summary> Get Saving By Id </summary>
        /// <param name="savingId"></param>
        [HttpGet("{savingId}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetSavingByIdAsync([FromRoute] Guid savingId)
        {
            Guid userId = HttpContext.User.GetUserId();

            Validator.New()
                .When(savingId == default, "O Id da poupança é invalido.")
                .TriggerBadRequestExceptionIfExist();

            SavingDTO savingDTO = await _savingService.GetSavingByIdAsync(savingId, userId);

            return Ok(savingDTO);
        }

        /// <summary> Create Saving </summary>
        /// <param name="savingDTO"></param>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> AddSavingAsync([FromBody] SavingDTO savingDTO)
        {
            Guid userId = HttpContext.User.GetUserId();

            savingDTO = await _savingService.AddSavingAsync(savingDTO, userId);
            return Ok(savingDTO);
        }

        /// <summary> Update Saving </summary>
        /// <param name="savingId"></param>
        /// <param name="savingDTO"></param>
        [HttpPut("{savingId}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateSavingAsync([FromRoute] Guid savingId, [FromBody] SavingDTO savingDTO)
        {
            Guid userId = HttpContext.User.GetUserId();

            Validator.New()
                .When(savingId == default, "O Id da poupança é invalido.")
                .TriggerBadRequestExceptionIfExist();

            savingDTO.Id = savingId;

            savingDTO = await _savingService.UpdateSavingAsync(savingDTO, userId);
            return Ok(savingDTO);
        }

        /// <summary> Delete Savings </summary>
        /// <param name="savingIds"></param>
        [HttpPost("delete")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteSavingsAsync([FromBody] List<Guid> savingIds)
        {
            Guid userId = HttpContext.User.GetUserId();

            List<BaseResponseDTO> baseResponses = await _savingService.DeleteSavingsAsync(savingIds, userId);

            return Ok(baseResponses);
        }
        #endregion
    }
}

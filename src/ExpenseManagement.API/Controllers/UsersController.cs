namespace ExpenseManagement.API.Controllers
{
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Private variables
        private readonly IUserService _userService;
        #endregion

        #region Constructor
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region CRUD Methods

        /// <summary> Get All Users </summary>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            List<UserDTO> users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        /// <summary> Get User By Id </summary>
        /// <param name="userId"></param>
        [Authorize(Roles = "Admin")]
        [HttpGet("{userId}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] Guid userId)
        {
            Validator.New()
                .When(userId == default, "O Id do utilizador é invalido.")
                .TriggerBadRequestExceptionIfExist();

            UserDTO userDTO = await _userService.GetUserByIdAsync(userId);

            return Ok(userDTO);
        }

        /// <summary> Get User Settings </summary>
        [Authorize]
        [HttpGet("settings")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetUserSettingsAsync()
        {
            Guid userId = HttpContext.User.GetUserId();

            UserDTO userDTO = await _userService.GetUserByIdAsync(userId);

            return Ok(userDTO);
        }

        /// <summary> Create User </summary>
        /// <param name="userDTO"></param>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> AddUserAsync([FromBody] UserDTO userDTO)
        {
            userDTO = await _userService.AddUserAsync(userDTO);
            return Ok(userDTO);
        }

        /// <summary> Update User </summary>
        /// <param name="userId"></param>
        /// <param name="userDTO"></param>
        [Authorize(Roles = "Admin")]
        [HttpPut("{userId}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] Guid userId, [FromBody] UserDTO userDTO)
        {
            Validator.New()
                .When(userId == default, "O Id do utilizador é invalido.")
                .TriggerBadRequestExceptionIfExist();

            userDTO.Id = userId;

            userDTO = await _userService.UpdateUserAsync(userDTO);
            return Ok(userDTO);
        }

        /// <summary> Update User Password</summary>
        /// <param name="userId"></param>
        /// <param name="userDTO"></param>
        [Authorize]
        [HttpPut("password")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateUserPasswordAsync([FromBody] UserPasswordDTO userPasswordDTO)
        {
            Guid userId = HttpContext.User.GetUserId();

            UserDTO userDTO = await _userService.UpdateUserPasswordAsync(userId, userPasswordDTO);
            return Ok(userDTO);
        }

        /// <summary> Delete Users </summary>
        /// <param name="userIds"></param>
        [Authorize(Roles = "Admin")]
        [HttpPost("delete")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteUsersAsync([FromBody] List<Guid> userIds)
        {
            List<BaseResponseDTO> baseResponses = await _userService.DeleteUsersAsync(userIds);

            return Ok(baseResponses);
        }
        #endregion
    }
}

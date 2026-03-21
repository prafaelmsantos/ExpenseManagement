namespace ExpenseManagement.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Private variables
        private readonly IAuthService _authService;
        #endregion

        #region Constructor
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        #endregion

        #region CRUD Methods

        /// <summary> Create User </summary>
        /// <param name="userLoginDTO"></param>
        [AllowAnonymous]
        [HttpPost("login")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> AddUserAsync([FromBody] UserLoginDTO userLoginDTO)
        {
            UserTokenDTO userTokenDTO = await _authService.LoginAsync(userLoginDTO);
            return Ok(userTokenDTO);
        }
        #endregion
    }
}

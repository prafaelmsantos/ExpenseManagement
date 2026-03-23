namespace ExpenseManagement.Application.Services.Identity
{
    public sealed class AuthService : IAuthService
    {
        #region Private variables
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<User> _signInManager;
        #endregion

        #region Constructor
        public AuthService(
            IUserService userService,
            ITokenService tokenService,
            SignInManager<User> signInManager)
        {
            _userService = userService;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }
        #endregion

        #region Public methods
        public async Task<UserTokenDTO> LoginAsync(UserLoginDTO userLoginDTO)
        {
            User user = await _userService.GetUserByUserNameAsync(userLoginDTO.UserName);

            await CheckPasswordSignInAsync(user, userLoginDTO.Password);

            List<Claim> claims = await CreateUserClaimsAsync(user);

            return _tokenService.GenerateToken(claims);
        }
        #endregion

        #region Private methods
        private async Task CheckPasswordSignInAsync(User user, string password)
        {
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);

            Validator.New()
                .When(!signInResult.Succeeded, "Utilizador ou Password inválidos!")
                .TriggerUnauthorizedExceptionIfExist();
        }

        private async Task<List<Claim>> CreateUserClaimsAsync(User user)
        {
            ClaimsPrincipal claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);

            return [.. claimsPrincipal.Claims];
        }
        #endregion
    }
}

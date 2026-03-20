namespace ExpenseManagement.Application.Services
{
    public class InitialSyncService : IInitialSyncService
    {
        #region Private variables
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        #endregion

        #region Constructor
        public InitialSyncService(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }
        #endregion

        #region Public methods
        public async Task AddDefaultUserRolesAsync()
        {
            await _roleService.AddDefaultRolesAsync();
            await _userService.AddDefaultUserAsync();
        }
        #endregion
    }
}

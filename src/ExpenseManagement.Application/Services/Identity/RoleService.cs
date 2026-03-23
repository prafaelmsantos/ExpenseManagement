namespace ExpenseManagement.Application.Services.Identity
{
    public sealed class RoleService : IRoleService
    {
        #region Private variables
        private readonly RoleManager<Role> _roleManager;
        private readonly UserDefaultSettings _userDefaultSettings;
        #endregion

        #region Constructor
        public RoleService(RoleManager<Role> roleManager, IOptions<UserDefaultSettings> options)
        {
            _roleManager = roleManager;
            _userDefaultSettings = options.Value;
        }
        #endregion

        #region Public methods
        public async Task AddDefaultRolesAsync()
        {
            await AddDefaultRoleAsync(_userDefaultSettings.RoleAdmin);
            await AddDefaultRoleAsync(_userDefaultSettings.Role);
        }
        #endregion

        #region Private methods
        private async Task AddDefaultRoleAsync(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                Role role = new(name: roleName);
                IdentityResult result = await _roleManager.CreateAsync(role);

                Validator.New()
                    .When(!result.Succeeded, "Erro ao tentar criar o role.")
                    .TriggerBadRequestExceptionIfExist();
            }
        }
        #endregion
    }
}

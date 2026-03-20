namespace ExpenseManagement.Application.Services
{
    public class InitialSyncService : IInitialSyncService
    {
        #region Private variables
        private readonly ILogger<InitialSyncService> _logger;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private const string DefaultUserName = "admin";
        private const string DefaultRoleName = "Admin";
        private const string DefaultPassword = "admin123";
        #endregion

        #region Constructor
        public InitialSyncService(ILogger<InitialSyncService> logger, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        #endregion

        public async Task AddDefaultUserRoleAsync()
        {
            try
            {
                if (!await _roleManager.RoleExistsAsync(DefaultRoleName))
                {
                    Role defaultRole = new(name: DefaultRoleName, isDefault: true);
                    IdentityResult roleResult = await _roleManager.CreateAsync(defaultRole);

                    if (!roleResult.Succeeded)
                    {
                        foreach (var error in roleResult.Errors)
                        {
                            _logger.LogError("Error creating role '{Role}': {Error}", DefaultRoleName, error.Description);
                        }

                        return;
                    }
                }

                User? defaultUser = await _userManager.FindByNameAsync(DefaultUserName);
                if (defaultUser is null)
                {
                    defaultUser = new User(email: DefaultUserName, isDefault: true);

                    IdentityResult userResult = await _userManager.CreateAsync(defaultUser, DefaultPassword);

                    if (!userResult.Succeeded)
                    {
                        foreach (var error in userResult.Errors)
                        {
                            _logger.LogError("Error creating role '{User}': {Error}", DefaultUserName, error.Description);
                        }

                        return;
                    }

                    IdentityResult addRoleResult = await _userManager.AddToRoleAsync(defaultUser, DefaultRoleName);
                    if (!addRoleResult.Succeeded)
                    {
                        foreach (var error in addRoleResult.Errors)
                        {
                            _logger.LogError("Error assigning user '{User}' to role '{Role}': {Error}", DefaultUserName, DefaultRoleName, error.Description);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to execute InitialSyncService: {Message}", ex.Message);
            }
        }
    }
}

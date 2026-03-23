namespace ExpenseManagement.Application.Services.Identity
{
    public sealed class UserService : IUserService
    {
        #region Private variables
        private readonly UserManager<User> _userManager;
        private readonly UserDefaultSettings _userDefaultSettings;
        #endregion

        #region Constructor
        public UserService(
            UserManager<User> userManager,
            IOptions<UserDefaultSettings> options)
        {
            _userManager = userManager;
            _userDefaultSettings = options.Value;
        }
        #endregion

        #region Public methods
        public async Task<List<UserTableDTO>> GetAllUsersAsync()
        {
            List<UserTableDTO> users = await _userManager.Users
                .Select(x => x.ToUserTableDTO())
                .ToListAsync();

            return users;
        }

        public async Task<UserDTO> GetUserByIdAsync(Guid userId)
        {
            User? user = await _userManager.FindByIdAsync(userId.ToString());

            Validator.New()
               .When(user is null, "Utilizador não encontrado.")
               .TriggerBadRequestExceptionIfExist();

            return user!.ToUserDTO();
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            User? user = await _userManager.FindByNameAsync(userName);

            Validator.New()
               .When(user is null, "Utilizador não encontrado.")
               .TriggerBadRequestExceptionIfExist();

            return user!;
        }

        public async Task<UserDTO> AddUserAsync(UserDTO userDTO)
        {
            User? user = await _userManager.FindByNameAsync(userDTO.UserName);

            Validator.New()
               .When(user is not null, "Um utilizador com o mesmo email já existe.")
               .TriggerBadRequestExceptionIfExist();

            user = new(firstName: userDTO.FirstName, lastName: userDTO.LastName, userName: userDTO.UserName);
            IdentityResult userResult = await _userManager.CreateAsync(user);

            Validator.New()
               .When(!userResult.Succeeded, "Erro ao tentar criar o utilizador.")
               .TriggerBadRequestExceptionIfExist();

            IdentityResult addRoleResult = await _userManager.AddToRoleAsync(user, _userDefaultSettings.Role);

            Validator.New()
               .When(!addRoleResult.Succeeded, "Erro ao tentar associar a role ao utilizador.")
               .TriggerBadRequestExceptionIfExist();

            return user.ToUserDTO();
        }

        public async Task AddDefaultUserAsync()
        {
            User? user = await _userManager.FindByNameAsync(_userDefaultSettings.UserName);
            if (user is null)
            {
                user = new User(userName: _userDefaultSettings.UserName);

                IdentityResult userResult = await _userManager.CreateAsync(user, _userDefaultSettings.Password);

                Validator.New()
                    .When(!userResult.Succeeded, "Erro ao tentar criar o utilizador.")
                    .TriggerBadRequestExceptionIfExist();

                IdentityResult addRoleResult = await _userManager.AddToRoleAsync(user, _userDefaultSettings.RoleAdmin);

                Validator.New()
                   .When(!addRoleResult.Succeeded, "Erro ao tentar associar a role ao utilizador.")
                   .TriggerBadRequestExceptionIfExist();
            }
        }

        public async Task<UserDTO> UpdateUserAsync(UserDTO userDTO)
        {
            User user = await GetUserByUserNameAsync(userDTO.UserName);

            user.Update(firstName: userDTO.FirstName, lastName: userDTO.LastName);
            IdentityResult userResult = await _userManager.UpdateAsync(user);

            Validator.New()
               .When(!userResult.Succeeded, "Erro ao tentar atualizar o utilizador.")
               .TriggerBadRequestExceptionIfExist();

            return user.ToUserDTO();
        }

        public async Task<List<BaseResponseDTO>> DeleteUsersAsync(List<Guid> userIds)
        {
            List<BaseResponseDTO> internalBaseResponseDTOs = [];

            foreach (Guid userId in userIds)
            {
                BaseResponseDTO internalBaseResponseDTO = new() { Id = userId, Success = false };
                try
                {
                    User? user = await _userManager.FindByIdAsync(userId.ToString());

                    if (user is not null)
                    {
                        if (user.UserRoles.Count != 0 && user.UserRoles.First().Role.Name == _userDefaultSettings.RoleAdmin)
                        {
                            internalBaseResponseDTO.Message = "O utilizador selecionado é o administrador do sistema.";
                        }
                        else
                        {
                            IdentityResult resultDelete = await _userManager.DeleteAsync(user);

                            if (resultDelete.Succeeded)
                            {
                                internalBaseResponseDTO.Success = resultDelete.Succeeded;
                            }
                            else
                            {
                                internalBaseResponseDTO.Message = "Erro ao tentar apagar o utilizador.";
                            }
                        }
                    }
                    else
                    {
                        internalBaseResponseDTO.Message = "Utilizador não encontrado.";
                    }
                }
                catch (Exception)
                {
                    internalBaseResponseDTO.Message = "Erro ao tentar apagar o utilizador.";
                }

                internalBaseResponseDTOs.Add(internalBaseResponseDTO);
            }

            return internalBaseResponseDTOs;
        }
        #endregion
    }
}

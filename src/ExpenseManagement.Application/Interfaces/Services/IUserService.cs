namespace ExpenseManagement.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<UserTableDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(Guid userId);
        Task<User> GetUserByUserNameAsync(string userName);
        Task<UserDTO> CreateUserAsync(UserDTO userDTO);
        Task<UserDTO> UpdateUserAsync(UserDTO userDTO);
        Task AddDefaultUserAsync();
    }
}

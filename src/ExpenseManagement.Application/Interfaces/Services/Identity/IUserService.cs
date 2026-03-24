namespace ExpenseManagement.Application.Interfaces.Services.Identity
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(Guid userId);
        Task<User> GetUserByUserNameAsync(string userName);
        Task<UserDTO> AddUserAsync(UserDTO userDTO);
        Task AddDefaultUserAsync();
        Task<UserDTO> UpdateUserAsync(UserDTO userDTO);
        Task<List<BaseResponseDTO>> DeleteUsersAsync(List<Guid> userIds);
    }
}

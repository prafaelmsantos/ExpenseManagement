namespace ExpenseManagement.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<UserTokenDTO> LoginAsync(UserLoginDTO userLoginDTO);
    }
}

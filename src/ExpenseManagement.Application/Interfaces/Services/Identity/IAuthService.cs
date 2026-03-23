namespace ExpenseManagement.Application.Interfaces.Services.Identity
{
    public interface IAuthService
    {
        Task<UserTokenDTO> LoginAsync(UserLoginDTO userLoginDTO);
    }
}

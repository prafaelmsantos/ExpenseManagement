namespace ExpenseManagement.Application.Interfaces.Services
{
    public interface ISignInService
    {
        Task<UserTokenDTO> LoginAsync(UserLoginDTO userLoginDTO);
    }
}

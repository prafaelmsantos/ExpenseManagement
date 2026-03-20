namespace ExpenseManagement.Application.Interfaces.Services
{
    public interface ITokenService
    {
        UserTokenDTO GenerateToken(List<Claim> claims);
    }
}

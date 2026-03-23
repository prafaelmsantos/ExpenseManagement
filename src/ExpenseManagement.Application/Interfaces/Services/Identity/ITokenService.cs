namespace ExpenseManagement.Application.Interfaces.Services.Identity
{
    public interface ITokenService
    {
        UserTokenDTO GenerateToken(List<Claim> claims);
    }
}

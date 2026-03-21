namespace ExpenseManagement.API.Extentions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            string claim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
            return Guid.Parse(claim!);
        }
    }
}

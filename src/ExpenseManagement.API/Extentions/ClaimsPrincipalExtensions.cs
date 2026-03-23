namespace ExpenseManagement.API.Extentions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            string claim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
            Guid userId = Guid.Parse(claim!);

            Validator.New()
                .When(userId == default, "O Id do utilizador é invalido.")
                .TriggerUnauthorizedExceptionIfExist();

            return userId;
        }
    }
}

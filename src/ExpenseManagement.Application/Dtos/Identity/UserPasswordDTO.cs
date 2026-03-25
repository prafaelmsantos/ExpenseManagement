namespace ExpenseManagement.Application.Dtos.Identity
{
    public sealed class UserPasswordDTO
    {
        public string CurrentPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
    }
}

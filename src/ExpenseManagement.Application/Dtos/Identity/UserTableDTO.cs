namespace ExpenseManagement.Application.Dtos.Identity
{
    public sealed class UserTableDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string? FullName { get; set; }
        public string Role { get; set; } = null!;
    }
}

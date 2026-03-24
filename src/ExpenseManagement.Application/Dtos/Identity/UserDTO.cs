namespace ExpenseManagement.Application.Dtos.Identity
{
    public sealed class UserDTO
    {
        public Guid? Id { get; set; }
        public string UserName { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public RoleDTO Role { get; set; } = null!;
    }
}

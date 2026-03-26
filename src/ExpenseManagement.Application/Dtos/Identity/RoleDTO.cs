namespace ExpenseManagement.Application.Dtos.Identity
{
    public sealed class RoleDTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = null!;
    }
}

namespace ExpenseManagement.Application.Mappers
{
    public static class RoleMapper
    {
        public static RoleDTO ToRoleDTO(this Role role)
        {
            if (role is null)
            {
                return null!;
            }

            return new RoleDTO()
            {
                Id = role.Id,
                Name = role.Name ?? string.Empty
            };
        }
    }
}

namespace ExpenseManagement.Application.Mappers.Identity
{
    public static class UserMapper
    {
        public static UserDTO ToUserDTO(this User user)
        {
            if (user is null || user.UserRoles.Count == 0)
            {
                return null!;
            }

            return new UserDTO()
            {
                Id = user.Id,
                UserName = user.UserName ?? string.Empty,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.UserRoles.First().Role.ToRoleDTO()
            };
        }
    }
}

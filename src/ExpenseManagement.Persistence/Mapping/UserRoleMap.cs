namespace ExpenseManagement.Persistence.Mapping
{
    public class UserRoleMap : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> entity)
        {
            entity.ToTable("users_roles");

            entity.HasKey(user => new { user.UserId, user.RoleId });

            entity.Property(x => x.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            entity.Property(x => x.RoleId)
                .HasColumnName("role_id")
                .IsRequired();
        }
    }
}

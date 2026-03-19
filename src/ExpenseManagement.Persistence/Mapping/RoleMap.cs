namespace ExpenseManagement.Persistence.Mapping
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> entity)
        {
            entity.ToTable("roles");

            entity.HasKey(x => x.Id);

            entity.HasIndex(r => r.NormalizedName)
                .IsUnique();

            entity.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            entity.Property(x => x.Name)
                .HasColumnName("name")
                .IsRequired(false);

            entity.Property(x => x.IsDefault)
                .HasColumnName("is_default")
                .HasDefaultValue(false)
                .IsRequired();

            entity.Property(x => x.NormalizedName)
                .HasColumnName("normalized_name")
                .IsRequired(false);

            entity.Property(x => x.ConcurrencyStamp)
                .HasColumnName("concurrency_stamp")
                .IsConcurrencyToken()
                .IsRequired(false);

            entity.HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            entity.HasMany(e => e.RoleClaims)
                .WithOne(e => e.Role)
                .HasForeignKey(rc => rc.RoleId)
                .IsRequired();
        }
    }
}

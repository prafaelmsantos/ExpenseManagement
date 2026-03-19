namespace ExpenseManagement.Persistence.Mapping
{
    public class UserLoginMap : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> entity)
        {
            entity.ToTable("user_logins");

            entity.HasKey(l => new { l.LoginProvider, l.ProviderKey });

            entity.Property(x => x.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            entity.Property(x => x.LoginProvider)
                .HasColumnName("login_provider")
                .HasMaxLength(128)
                .IsRequired();

            entity.Property(x => x.ProviderKey)
                .HasColumnName("provider_key")
                .HasMaxLength(128)
                .IsRequired();

            entity.Property(x => x.ProviderDisplayName)
                .HasColumnName("provider_display_name")
                .IsRequired(false);

        }
    }
}

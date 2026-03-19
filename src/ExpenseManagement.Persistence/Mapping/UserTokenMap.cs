namespace ExpenseManagement.Persistence.Mapping
{
    public class UserTokenMap : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> entity)
        {
            entity.ToTable("user_tokens");

            entity.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

            entity.Property(x => x.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            entity.Property(x => x.LoginProvider)
                .HasColumnName("login_provider")
                .IsRequired();

            entity.Property(x => x.Name)
                .HasColumnName("name")
                .IsRequired();

            entity.Property(x => x.Value)
                .HasColumnName("value")
                .IsRequired(false);

        }
    }
}

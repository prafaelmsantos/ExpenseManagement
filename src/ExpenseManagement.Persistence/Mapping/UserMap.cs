namespace ExpenseManagement.Persistence.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("users");

            entity.HasKey(x => x.Id);

            entity.HasIndex(u => u.NormalizedUserName).IsUnique();
            entity.HasIndex(u => u.NormalizedEmail);

            entity.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            entity.Property(x => x.UserName)
                .HasColumnName("user_name")
                .HasMaxLength(256)
                .IsRequired(false);

            entity.Property(x => x.NormalizedUserName)
                .HasColumnName("normalized_user_name")
                .HasMaxLength(256)
                .IsRequired(false);

            entity.Property(x => x.Email)
                .HasColumnName("email")
                .HasMaxLength(256)
                .IsRequired(false);

            entity.Property(x => x.NormalizedEmail)
                .HasColumnName("normalized_email")
                .HasMaxLength(256)
                .IsRequired(false);

            entity.Property(x => x.EmailConfirmed)
                .HasColumnName("email_confirmed")
                .HasDefaultValue(false)
                .IsRequired();

            entity.Property(x => x.PasswordHash)
                .HasColumnName("password_hash")
                .IsRequired(false);

            entity.Property(x => x.SecurityStamp)
                .HasColumnName("security_stamp")
                .IsRequired(false);

            entity.Property(x => x.ConcurrencyStamp)
                .HasColumnName("concurrency_stamp")
                .IsConcurrencyToken()
                .IsRequired(false);

            entity.Property(x => x.PhoneNumber)
                .HasColumnName("phone_number")
                .IsRequired(false);

            entity.Property(x => x.PhoneNumberConfirmed)
                .HasColumnName("phone_number_confirmed")
                .HasDefaultValue(false)
                .IsRequired();

            entity.Property(x => x.TwoFactorEnabled)
                .HasColumnName("two_factor_enabled")
                .HasDefaultValue(false)
                .IsRequired();

            entity.Property(x => x.LockoutEnd)
                .HasColumnName("lockout_end")
                .IsRequired(false);

            entity.Property(x => x.LockoutEnabled)
                .HasColumnName("lockout_enabled")
                .HasDefaultValue(false)
                .IsRequired();

            entity.Property(x => x.AccessFailedCount)
                .HasColumnName("access_failed_count")
                .IsRequired();

            entity.Property(x => x.FirstName)
                .HasColumnName("first_name")
                .IsRequired();

            entity.Property(x => x.LastName)
                .HasColumnName("last_name")
                .IsRequired();

            entity.Property(x => x.DarkMode)
                .HasColumnName("dark_mode")
                .HasDefaultValue(false)
                .IsRequired();

            entity.Property(x => x.IsDefault)
                .HasColumnName("is_default")
                .HasDefaultValue(false)
                .IsRequired();

            entity.HasMany(e => e.Claims)
                .WithOne(e => e.User)
                .HasForeignKey(uc => uc.UserId)
                .IsRequired();

            entity.HasMany(e => e.Logins)
                .WithOne(e => e.User)
                .HasForeignKey(ul => ul.UserId)
                .IsRequired();

            entity.HasMany(e => e.Tokens)
                .WithOne(e => e.User)
                .HasForeignKey(ut => ut.UserId)
                .IsRequired();

            entity.HasMany(e => e.UserRoles)
               .WithOne(e => e.User)
               .HasForeignKey(ur => ur.UserId)
               .IsRequired();
        }
    }
}

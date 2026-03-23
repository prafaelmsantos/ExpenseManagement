namespace ExpenseManagement.Persistence.Mapping.Identity
{
    public class UserClaimMap : IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> entity)
        {
            entity.ToTable("user_claims");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            entity.Property(x => x.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            entity.Property(x => x.ClaimType)
                .HasColumnName("claim_type")
                .IsRequired(false);

            entity.Property(x => x.ClaimValue)
                .HasColumnName("claim_value")
                .IsRequired(false);

        }
    }
}

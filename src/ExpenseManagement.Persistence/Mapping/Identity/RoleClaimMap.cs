namespace ExpenseManagement.Persistence.Mapping.Identity
{
    public class RoleClaimMap : IEntityTypeConfiguration<RoleClaim>
    {
        public void Configure(EntityTypeBuilder<RoleClaim> entity)
        {
            entity.ToTable("role_claims");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            entity.Property(x => x.RoleId)
                .HasColumnName("role_id")
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

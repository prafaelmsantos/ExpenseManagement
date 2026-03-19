namespace ExpenseManagement.Persistence.Mapping.Base
{
    public class BaseEntityMap<TEntity> : EntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public override void Map(EntityTypeBuilder<TEntity> entity)
        {
            entity.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired(true);

            entity.HasKey(x => x.Id);

            entity.HasIndex(x => x.Id)
                .IsUnique();
        }
    }
}

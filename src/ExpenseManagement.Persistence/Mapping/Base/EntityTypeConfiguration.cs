namespace ExpenseManagement.Persistence.Mapping.Base
{
    public abstract class EntityTypeConfiguration<TEntity> where TEntity : class
    {
        public abstract void Map(EntityTypeBuilder<TEntity> entity);
    }
}

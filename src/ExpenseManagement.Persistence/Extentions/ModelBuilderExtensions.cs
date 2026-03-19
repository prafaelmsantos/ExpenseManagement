namespace ExpenseManagement.Persistence.Extentions
{
    public static class ModelBuilderExtensions
    {
        public static void AddConfiguration<TEntity>(
            this ModelBuilder modelBuilder,
            EntityTypeConfiguration<TEntity> configuration) where TEntity : class
        {
            configuration.Map(modelBuilder.Entity<TEntity>());
        }

        public static void ApplyConfiguration<TEntity>(
            this ModelBuilder modelBuilder,
            IEntityTypeConfiguration<TEntity> configuration) where TEntity : class
        {
            configuration.Configure(modelBuilder.Entity<TEntity>());
        }
    }
}

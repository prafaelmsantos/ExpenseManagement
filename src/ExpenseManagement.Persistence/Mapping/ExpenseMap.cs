namespace ExpenseManagement.Persistence.Mapping
{
    public partial class ExpenseMap : BaseEntityMap<Expense>
    {
        public override void Map(EntityTypeBuilder<Expense> entity)
        {
            base.Map(entity);

            entity.ToTable("expense");

            entity.Property(x => x.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            entity.Property(x => x.Name)
                .HasColumnName("name")
                .IsRequired();

            entity.Property(x => x.Amount)
                .HasColumnName("amount")
                .IsRequired();

            entity.Property(x => x.Category)
                .HasColumnName("category")
                .IsRequired();

            entity.Property(x => x.Date)
                .HasColumnName("date")
                .IsRequired();

            entity.Property(x => x.Description)
                .HasColumnName("description")
                .IsRequired(false);
        }
    }
}

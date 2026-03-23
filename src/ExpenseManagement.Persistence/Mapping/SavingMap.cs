namespace ExpenseManagement.Persistence.Mapping
{
    public partial class SavingMap : BaseEntityMap<Saving>
    {
        public override void Map(EntityTypeBuilder<Saving> entity)
        {
            base.Map(entity);

            entity.ToTable("saving");

            entity.Property(x => x.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            entity.Property(x => x.Amount)
                .HasColumnName("amount")
                .IsRequired();

            entity.Property(x => x.Date)
                .HasColumnName("date")
                .IsRequired();
        }
    }
}

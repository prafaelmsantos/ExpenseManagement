namespace ExpenseManagement.Persistence.Repositories
{
    public sealed class SavingRepository : Repository<Saving>, ISavingRepository
    {
        public SavingRepository(AppDbContext context) : base(context) { }
    }
}

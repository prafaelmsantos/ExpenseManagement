namespace ExpenseManagement.Persistence.Repositories
{
    public sealed class ExpenseRepository : Repository<Expense>, IExpenseRepository
    {
        public ExpenseRepository(AppDbContext context) : base(context) { }
    }
}

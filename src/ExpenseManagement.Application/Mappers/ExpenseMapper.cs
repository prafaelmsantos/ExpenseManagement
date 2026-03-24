namespace ExpenseManagement.Application.Mappers
{
    public static class ExpenseMapper
    {
        public static ExpenseDTO ToExpenseDTO(this Expense expense)
        {
            if (expense is null)
            {
                return null!;
            }

            return new ExpenseDTO()
            {
                Id = expense.Id,
                Name = expense.Name,
                Amount = expense.Amount,
                Category = expense.Category,
                Date = expense.Date,
                Description = expense.Description
            };
        }

        public static ExpenseTableDTO ToExpenseTableDTO(this Expense expense)
        {
            if (expense is null)
            {
                return null!;
            }

            return new ExpenseTableDTO()
            {
                Id = expense.Id,
                Amount = expense.Amount,
                Category = expense.Category,
                Date = expense.Date.ToString("dd-MM-yyyy HH:mm:ss")
            };
        }
    }
}

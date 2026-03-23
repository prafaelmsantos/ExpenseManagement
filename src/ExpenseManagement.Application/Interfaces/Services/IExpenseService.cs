namespace ExpenseManagement.Application.Interfaces.Services
{
    public interface IExpenseService
    {
        Task<List<ExpenseTableDTO>> GetAllExpensesAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<ExpenseDTO> GetExpenseByIdAsync(Guid expenseIdGuid, Guid userId, CancellationToken cancellationToken = default);
        Task<ExpenseDTO> AddExpenseAsync(ExpenseDTO expenseDTO, Guid userId, CancellationToken cancellationToken = default);
        Task<ExpenseDTO> UpdateExpenseAsync(ExpenseDTO expenseDTO, Guid userId, CancellationToken cancellationToken = default);
        Task<List<BaseResponseDTO>> DeleteExpensesAsync(List<Guid> expenseIds, Guid userId, CancellationToken cancellationToken = default);
    }
}

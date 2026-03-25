namespace ExpenseManagement.Application.Services
{
    public class ExpenseService : IExpenseService
    {
        #region Private variables
        private readonly IExpenseRepository _expenseRepository;
        #endregion

        #region Constructor
        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }
        #endregion

        #region Public methods
        public async Task<List<ExpenseDTO>> GetAllExpensesAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            List<ExpenseDTO> expenses = await _expenseRepository
                .GetAllQueryable()
                .Where(x => x.UserId == userId)
                .Select(x => x.ToExpenseDTO())
                .ToListAsync(cancellationToken);

            return expenses;
        }

        public async Task<ExpenseDTO> GetExpenseByIdAsync(Guid expenseId, Guid userId, CancellationToken cancellationToken = default)
        {
            Expense? expense = await _expenseRepository
               .GetAllQueryable()
               .Where(x => x.Id == expenseId && x.UserId == userId)
               .FirstOrDefaultAsync(cancellationToken);

            Validator.New()
               .When(expense is null, "Despesa não encontrada.")
               .TriggerBadRequestExceptionIfExist();

            return expense!.ToExpenseDTO();
        }

        public async Task<ExpenseDTO> AddExpenseAsync(ExpenseDTO expenseDTO, Guid userId, CancellationToken cancellationToken = default)
        {
            Expense expense = new(
                userId: userId,
                name: expenseDTO.Name,
                amount: expenseDTO.Amount,
                category: expenseDTO.Category,
                date: expenseDTO.Date,
                description: !string.IsNullOrWhiteSpace(expenseDTO.Description) ? expenseDTO.Description : null);

            expense = await _expenseRepository.AddAsync(expense, cancellationToken);

            return expense.ToExpenseDTO();
        }

        public async Task<ExpenseDTO> UpdateExpenseAsync(ExpenseDTO expenseDTO, Guid userId, CancellationToken cancellationToken = default)
        {
            Expense? expense = await _expenseRepository
                .GetAllQueryable()
                .Where(x => x.Id == expenseDTO.Id && x.UserId == userId)
                .FirstOrDefaultAsync(cancellationToken);

            Validator.New()
                .When(expense is null, "Despesa não encontrada.")
                .TriggerBadRequestExceptionIfExist();

            expense!.Update(
                name: expenseDTO.Name,
                amount: expenseDTO.Amount,
                category: expenseDTO.Category,
                date: expenseDTO.Date,
                description: !string.IsNullOrWhiteSpace(expenseDTO.Description) ? expenseDTO.Description : null);

            expense = await _expenseRepository.UpdateAsync(expense, cancellationToken);

            return expense.ToExpenseDTO();
        }

        public async Task<List<BaseResponseDTO>> DeleteExpensesAsync(List<Guid> expenseIds, Guid userId, CancellationToken cancellationToken = default)
        {
            List<BaseResponseDTO> internalBaseResponseDTOs = [];

            foreach (Guid expenseId in expenseIds)
            {
                BaseResponseDTO internalBaseResponseDTO = new() { Id = expenseId, Success = false };
                try
                {
                    Expense? expense = await _expenseRepository
                        .GetAllQueryable()
                        .Where(x => x.Id == expenseId && x.UserId == userId)
                        .FirstOrDefaultAsync(cancellationToken);

                    if (expense is not null)
                    {
                        await _expenseRepository.RemoveAsync(expense, cancellationToken);
                        internalBaseResponseDTO.Success = true;
                    }
                    else
                    {
                        internalBaseResponseDTO.Message = "Despesa não encontrada.";
                    }
                }
                catch (Exception)
                {
                    internalBaseResponseDTO.Message = "Erro ao tentar apagar a despesa.";
                }

                internalBaseResponseDTOs.Add(internalBaseResponseDTO);
            }

            return internalBaseResponseDTOs;
        }
        #endregion
    }
}

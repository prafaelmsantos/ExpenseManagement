namespace ExpenseManagement.Application.Services
{
    public sealed class SavingService : ISavingService
    {
        #region Private variables
        private readonly ISavingRepository _savingRepository;
        #endregion

        #region Constructor
        public SavingService(ISavingRepository savingRepository)
        {
            _savingRepository = savingRepository;
        }
        #endregion

        #region Public methods
        public async Task<List<SavingTableDTO>> GetAllSavingsAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            List<SavingTableDTO> savings = await _savingRepository
                .GetAllQueryable()
                .Where(x => x.UserId == userId)
                .Select(x => x.ToSavingTableDTO())
                .ToListAsync(cancellationToken);

            return savings;
        }

        public async Task<SavingDTO> GetSavingByIdAsync(Guid savingId, Guid userId, CancellationToken cancellationToken = default)
        {
            Saving? saving = await _savingRepository
               .GetAllQueryable()
               .Where(x => x.Id == savingId && x.UserId == userId)
               .FirstOrDefaultAsync(cancellationToken);

            Validator.New()
               .When(saving is null, "Poupança não encontrada.")
               .TriggerBadRequestExceptionIfExist();

            return saving!.ToSavingDTO();
        }

        public async Task<SavingDTO> AddSavingAsync(SavingDTO savingDTO, Guid userId, CancellationToken cancellationToken = default)
        {
            Saving saving = new(
                userId: userId,
                name: savingDTO.Name,
                amount: savingDTO.Amount,
                category: savingDTO.Category,
                date: savingDTO.Date,
                description: savingDTO.Description);

            saving = await _savingRepository.AddAsync(saving, cancellationToken);

            return saving.ToSavingDTO();
        }

        public async Task<SavingDTO> UpdateSavingAsync(SavingDTO savingDTO, Guid userId, CancellationToken cancellationToken = default)
        {
            Saving? saving = await _savingRepository
                .GetAllQueryable()
                .Where(x => x.Id == savingDTO.Id && x.UserId == userId)
                .FirstOrDefaultAsync(cancellationToken);

            Validator.New()
                .When(saving is null, "Poupança não encontrada.")
                .TriggerBadRequestExceptionIfExist();

            saving!.Update(
                name: savingDTO.Name,
                amount: savingDTO.Amount,
                category: savingDTO.Category,
                date: savingDTO.Date,
                description: savingDTO.Description);

            saving = await _savingRepository.UpdateAsync(saving, cancellationToken);

            return saving.ToSavingDTO();
        }

        public async Task<List<BaseResponseDTO>> DeleteSavingsAsync(List<Guid> savingIds, Guid userId, CancellationToken cancellationToken = default)
        {
            List<BaseResponseDTO> internalBaseResponseDTOs = [];

            foreach (Guid savingId in savingIds)
            {
                BaseResponseDTO internalBaseResponseDTO = new() { Id = savingId, Success = false };
                try
                {
                    Saving? saving = await _savingRepository
                        .GetAllQueryable()
                        .Where(x => x.Id == savingId && x.UserId == userId)
                        .FirstOrDefaultAsync(cancellationToken);

                    if (saving is not null)
                    {
                        await _savingRepository.RemoveAsync(saving, cancellationToken);
                        internalBaseResponseDTO.Success = true;
                    }
                    else
                    {
                        internalBaseResponseDTO.Message = "Poupança não encontrada.";
                    }
                }
                catch (Exception)
                {
                    internalBaseResponseDTO.Message = "Erro ao tentar apagar a poupança.";
                }

                internalBaseResponseDTOs.Add(internalBaseResponseDTO);
            }

            return internalBaseResponseDTOs;
        }
        #endregion
    }
}

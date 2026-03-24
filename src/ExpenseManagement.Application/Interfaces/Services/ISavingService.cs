namespace ExpenseManagement.Application.Interfaces.Services
{
    public interface ISavingService
    {
        Task<List<SavingDTO>> GetAllSavingsAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<SavingDTO> GetSavingByIdAsync(Guid savingId, Guid userId, CancellationToken cancellationToken = default);
        Task<SavingDTO> AddSavingAsync(SavingDTO savingDTO, Guid userId, CancellationToken cancellationToken = default);
        Task<SavingDTO> UpdateSavingAsync(SavingDTO savingDTO, Guid userId, CancellationToken cancellationToken = default);
        Task<List<BaseResponseDTO>> DeleteSavingsAsync(List<Guid> savingIds, Guid userId, CancellationToken cancellationToken = default);
    }
}

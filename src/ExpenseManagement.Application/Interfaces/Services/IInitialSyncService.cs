namespace ExpenseManagement.Application.Interfaces.Services
{
    public interface IInitialSyncService
    {
        Task AddDefaultUserRoleAsync();
    }
}

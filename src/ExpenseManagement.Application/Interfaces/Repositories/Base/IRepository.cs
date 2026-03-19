namespace ExpenseManagement.Application.Interfaces.Repositories.Base
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        #region Create
        Task<TEntity> AddAsync(
            TEntity entity,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<TEntity>> AddRangeAsync(
            IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default);
        #endregion

        #region Read
        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<TEntity>?> GetAllIncludingAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Expression<Func<TEntity, object>>[]? includeProperties = null,
            CancellationToken cancellationToken = default);

        IQueryable<TEntity> GetAllQueryable();
        #endregion

        #region Update
        Task<TEntity> UpdateAsync(
            TEntity entity,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<TEntity>> UpdateRangeAsync(
            IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default);
        #endregion

        #region Delete
        Task RemoveAsync(
            TEntity entity,
            CancellationToken cancellationToken = default);

        Task<bool> RemoveRangeAsync(
            IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default);
        #endregion
    }
}

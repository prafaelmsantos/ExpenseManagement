namespace ExpenseManagement.Persistence.Repositories.Base
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        #region Properties
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _entity;
        #endregion

        #region Constructor
        public Repository(DbContext context)
        {
            _context = context;
            _entity = context.Set<TEntity>();
        }
        #endregion

        #region Create
        public async Task<TEntity> AddAsync(
            TEntity entity,
            CancellationToken cancellationToken = default)
        {
            _entity.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task<IEnumerable<TEntity>> AddRangeAsync(
            IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default)
        {
            _entity.AddRange(entities);
            await _context.SaveChangesAsync(cancellationToken);
            return entities;
        }
        #endregion

        #region Read
        public async Task<TEntity?> GetByIdAsync(Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _entity.FindAsync([id], cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _entity.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _entity;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>?> GetAllIncludingAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Expression<Func<TEntity, object>>[]? includeProperties = null,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _entity;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {

                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.ToListAsync(cancellationToken);
        }

        public IQueryable<TEntity> GetAllQueryable()
        {
            return _entity;
        }
        #endregion

        #region Update
        public async Task<TEntity> UpdateAsync(
            TEntity entity,
            CancellationToken cancellationToken = default)
        {
            _entity.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task<IEnumerable<TEntity>> UpdateRangeAsync(
            IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default)
        {
            _entity.UpdateRange(entities);
            await _context.SaveChangesAsync(cancellationToken);
            return entities;
        }
        #endregion

        #region Delete

        public async Task RemoveAsync(
            TEntity entity,
            CancellationToken cancellationToken = default)
        {
            _entity.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> RemoveRangeAsync(
            IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default)
        {
            _entity.RemoveRange(entities);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        #endregion
    }
}

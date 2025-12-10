using Microsoft.EntityFrameworkCore;
using MiniBlogApp.Data.Interfaces;

namespace MiniBlogApp.Data.Repositories;

public class RepositoryBase<TEntity>(ApplicationDbContext applicationDbContext, DbSet<TEntity> entities) : IRepositoryBase<TEntity> where TEntity : class
{
    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await entities.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await entities.FindAsync([id], cancellationToken); 
	}

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await entities.AddAsync(entity, cancellationToken);
    }

    public void Update(TEntity entity)
    {
        entities.Update(entity);
    }

    public void Remove(TEntity entity)
    {
        entities.Remove(entity);
    }
}

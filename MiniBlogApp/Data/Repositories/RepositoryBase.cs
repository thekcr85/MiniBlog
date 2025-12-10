using Microsoft.EntityFrameworkCore;
using MiniBlogApp.Data.Interfaces;

namespace MiniBlogApp.Data.Repositories;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<TEntity> _set;

    public RepositoryBase(ApplicationDbContext context)
    {
        _context = context;
        _set = context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _set.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _set.FindAsync(new object[] { id }, cancellationToken); 
	}

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _set.AddAsync(entity, cancellationToken);
    }

    public void Update(TEntity entity)
    {
        _set.Update(entity);
    }

    public void Remove(TEntity entity)
    {
        _set.Remove(entity);
    }
}

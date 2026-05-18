using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities.Base;
using TaskManager.Domain.Interfaces.Base;

namespace TaskManager.Infrastructure.Persistence;

public class Repository<T> : IRepository<T> where T : Entity
{
    TaskManagerDbContext _dbContext;

    public Repository(TaskManagerDbContext dbContext) => _dbContext = dbContext;
    
    public async Task CreateAsync(T entity, CancellationToken cancellationToken)
    {
        await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
    }

    public async Task<IEnumerable<T?>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<T?>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        if(id == Guid.Empty)
            throw new ArgumentNullException(nameof(id));
        
        var entity = await _dbContext.Set<T>().FindAsync(id);
        
        if(entity == null)
            throw new KeyNotFoundException();

        return await _dbContext.Set<T>().ToListAsync(cancellationToken);
    }

    public Task Update(T entity, CancellationToken cancellationToken)
    {
        _dbContext.Set<T>().Update(entity);
        return Task.CompletedTask;
    }

    public Task Delete(T entity, CancellationToken cancellationToken)
    {
        _dbContext.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }
}
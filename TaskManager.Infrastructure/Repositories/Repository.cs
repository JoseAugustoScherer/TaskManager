using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities.Base;
using TaskManager.Domain.Interfaces.Base;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Repositories;

public class Repository<T>(TaskManagerDbContext dbContext) : IRepository<T> where T : Entity
{
    public async Task CreateAsync(T entity, CancellationToken cancellationToken)
    {
        await dbContext.Set<T>().AddAsync(entity, cancellationToken);
    }

    public async Task<IEnumerable<T?>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        if(id == Guid.Empty)
           throw new ArgumentException("Id cannot be empty", nameof(id));
        
        return await dbContext.Set<T>().FindAsync(id, cancellationToken);
    }

    public Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        dbContext.Set<T>().Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        dbContext.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }
}
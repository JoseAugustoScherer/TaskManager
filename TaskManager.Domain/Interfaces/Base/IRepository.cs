using TaskManager.Domain.Entities.Base;

namespace TaskManager.Domain.Interfaces.Base;

public interface IRepository<T> where T : Entity
{
    public Task CreateAsync(T entity, CancellationToken cancellationToken);
    public Task<IEnumerable<T?>> GetAllAsync(CancellationToken cancellationToken);
    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task UpdateAsync(T entity, CancellationToken cancellationToken);
    public Task DeleteAsync(T entity, CancellationToken cancellationToken);
}
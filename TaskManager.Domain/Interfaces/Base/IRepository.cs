using TaskManager.Domain.Entities.Base;

namespace TaskManager.Domain.Interfaces.Base;

public interface IRepository<T> where T : Entity
{
    public Task CreateAsync(T entity, CancellationToken cancellationToken);
    public Task<IEnumerable<T?>> GetAllAsync(CancellationToken cancellationToken);
    public Task<IEnumerable<T?>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task UpdateAsync(Guid id, CancellationToken cancellationToken);
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
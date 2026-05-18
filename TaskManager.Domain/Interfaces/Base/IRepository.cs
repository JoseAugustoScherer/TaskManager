using TaskManager.Domain.Entities.Base;

namespace TaskManager.Domain.Interfaces.Base;

public interface IRepository<T> where T : Entity
{
    public Task CreateAsync(T entity, CancellationToken cancellationToken);
    public Task<IEnumerable<T?>> GetAllAsync(CancellationToken cancellationToken);
    public Task<IEnumerable<T?>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task Update(T entity, CancellationToken cancellationToken);
    public Task Delete(T entity, CancellationToken cancellationToken);
}
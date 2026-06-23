using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces.Base;

namespace TaskManager.Domain.Interfaces;

public interface ITaskItemRepository : IRepository<TaskItem>
{
    public Task<TaskItem?> GetTaskItemByIdWithOwnerAsync(Guid taskItemId, CancellationToken cancellationToken);
    public Task<IEnumerable<TaskItem?>> GetAllTaskItemWithOwnerAsync(CancellationToken cancellationToken);
    public Task<IEnumerable<TaskItem?>> GetTaskItemsByStatusWithOwnerAsync(TaskStatus taskStatus, CancellationToken cancellationToken);
}
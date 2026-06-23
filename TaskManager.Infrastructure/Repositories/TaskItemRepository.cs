using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Repositories;

public class TaskItemRepository(
    TaskManagerDbContext context
    ) : Repository<TaskItem>(context), ITaskItemRepository
{
    private readonly TaskManagerDbContext _context = context;

    public async Task<TaskItem?> GetTaskItemByIdWithOwnerAsync(Guid taskItemId, CancellationToken cancellationToken)
    {
        return await _context.Tasks
            .Include(x => x.Owner)
            .FirstOrDefaultAsync(x => x.Id == taskItemId, cancellationToken);
    }
    
    public async Task<IEnumerable<TaskItem?>> GetAllTaskItemWithOwnerAsync(CancellationToken cancellationToken)
    {
        return await _context.Tasks
            .Include(x => x.Owner).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TaskItem?>> GetTaskItemsByStatusWithOwnerAsync(TaskStatus taskStatus, CancellationToken cancellationToken)
    {
        return await _context.Tasks
            .Where(x => x.Status == taskStatus)
            .Include(x => x.Owner)
            .ToListAsync(cancellationToken);
    }
}
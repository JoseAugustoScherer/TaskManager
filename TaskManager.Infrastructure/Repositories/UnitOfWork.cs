using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities.Base;
using TaskManager.Domain.Interfaces;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Infrastructure.Repositories;

public class UnitOfWork(TaskManagerDbContext dbContext) : IUnitOfWork
{
    public Task<int> CommitAsync(CancellationToken cancellationToken)
    {
        UpdateAuditableEntities();
        return dbContext.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditableEntities()
    {
        var entries = dbContext.ChangeTracker.Entries<Entity>();

        foreach (var entry in entries)
        {
            if(entry.State == EntityState.Added)
                entry.Entity.CreatedAt = DateTime.UtcNow;
            
            if (entry.State == EntityState.Modified)
                entry.Entity.ModifiedAt = DateTime.UtcNow;
            
            entry.Property(x => x.CreatedAt).IsModified = false;
        }
    }
}
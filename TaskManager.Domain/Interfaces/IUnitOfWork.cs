namespace TaskManager.Domain.Interfaces;

public interface IUnitOfWork
{
    public Task<int> CommitAsync(CancellationToken cancellationToken);
}
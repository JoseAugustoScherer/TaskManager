using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces.Base;

namespace TaskManager.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    public Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken);
}
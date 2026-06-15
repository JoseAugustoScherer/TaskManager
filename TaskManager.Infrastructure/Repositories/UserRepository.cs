using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;
using TaskManager.Infrastructure.Data;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Repositories;

public class UserRepository(
    TaskManagerDbContext context
) : Repository<User>(context), IUserRepository
{
    private readonly TaskManagerDbContext _context = context;

    public async Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken: cancellationToken);
        
        return user;
    }
}
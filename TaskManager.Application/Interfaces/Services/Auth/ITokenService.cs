namespace TaskManager.Application.Interfaces.Services.Auth;

using User = TaskManager.Domain.Entities.User;

public interface ITokenService
{
    public string GenerateToken(User user);
}
namespace TaskManager.Application.Interfaces;

public interface IHashService
{
    public string Hash(string password);
    public bool Verify(string password, string passwordHash);
}
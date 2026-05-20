using System.Security.Cryptography;
using TaskManager.Application.Interfaces;

namespace TaskManager.Infrastructure.Services.Password;

public class PasswordHashing : IHashService
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100_000;
    
    private readonly HashAlgorithmName _algorithm =  HashAlgorithmName.SHA512;
    
    public string Hash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _algorithm, HashSize);

        var passwordHash = $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
        
        return passwordHash;
    }

    public bool Verify(string password, string passwordHash)
    {
        var parts = passwordHash.Split("-");
        var hash = Convert.FromHexString(parts[0]);
        var salt = Convert.FromHexString(parts[1]);

        var inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _algorithm, HashSize);
        
        return CryptographicOperations.FixedTimeEquals(hash, inputHash);
    }
}
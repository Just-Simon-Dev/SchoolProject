using System.Security.Cryptography;
using System.Text;
using SchoolProject.Application.Settings;

namespace SchoolProject.Application.services;

public interface IHashService
{
    bool VerifyPassword(string password, string hashedPassword);
    Task<string> HashPassword(string password);
}

public class HashService : IHashService
{
    const int keySize = 64;
    const int iterations = 350000;
    HashSettings hashSettings = new();
    HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
    
    public bool VerifyPassword(string password, string hashedPassword)
    {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, hashSettings.Salt, iterations, hashAlgorithm, keySize);
        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hashedPassword));
    }

    public async Task<string> HashPassword(string password)
    {
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            hashSettings.Salt,
            iterations,
            hashAlgorithm,
            keySize);
        return Convert.ToHexString(hash);
    }
}
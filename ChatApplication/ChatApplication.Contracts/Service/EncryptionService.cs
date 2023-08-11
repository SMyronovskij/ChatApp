using System.Security.Cryptography;
using System.Text;

namespace ChatApplication.Contracts.Service;

public class EncryptionService : IEncryptionService
{
    public const string salt = "salt";

    public string GetHashedString(string plainText)
    {
        var sha256 = SHA256.Create();

        var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(plainText));
        var hashSum = string.Empty;
        foreach (var b in hash)
        {
            hashSum += b.ToString("x2");
        }

        return Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(hashSum + salt));
    }
}
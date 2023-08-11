namespace ChatApplication.Contracts.Service;

public interface IEncryptionService
{
    string GetHashedString(string plainText);
}
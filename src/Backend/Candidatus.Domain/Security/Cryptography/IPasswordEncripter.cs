namespace Candidatus.Domain.Security.Cryptography;
public interface IPasswordEncripter
{
    public string Encrypt(string password);
    public bool Decrypt(string password, string passwordHashed);
}

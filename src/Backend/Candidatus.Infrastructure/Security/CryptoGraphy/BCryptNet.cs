using Candidatus.Domain.Security.Cryptography;

namespace Candidatus.Infrastructure.Security.CryptoGraphy;
public class BCryptNet : IPasswordEncripter
{
    public string Encrypt(string password) => BCrypt.Net.BCrypt.HashPassword(password);

    public bool Decrypt(string password, string passwordHashed) => BCrypt.Net.BCrypt.Verify(password, passwordHashed);
}

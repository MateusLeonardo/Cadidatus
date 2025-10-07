using Candidatus.Domain.Security.Cryptography;
using Candidatus.Infrastructure.Security.CryptoGraphy;

namespace CommonTestUtilities.Cryptography;

public class PasswordEncripterBuilder
{
    public static IPasswordEncripter Build()
    {
        return new BCryptNet();
    }
}
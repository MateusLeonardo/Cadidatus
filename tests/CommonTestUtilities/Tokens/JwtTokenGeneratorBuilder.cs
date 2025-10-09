using Candidatus.Domain.Security.Tokens;
using Candidatus.Infrastructure.Security.Tokens.Access.Generator;

namespace CommonTestUtilities.Tokens;

public class JwtTokenGeneratorBuilder
{
    public static IAccessTokenGenerator Buid()
    {
        return new JwtTokenGenerator(5, "tttttttttttttttttttttttttttttttt");
    }
}
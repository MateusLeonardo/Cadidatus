using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Candidatus.Infrastructure.Security.Tokens.Access;

public abstract class JwtTokenHandler
{
    protected static SymmetricSecurityKey SecurityKey(string signinKey)
    {
        var bytes = Encoding.UTF8.GetBytes(signinKey);

        return new SymmetricSecurityKey(bytes);
    }
}
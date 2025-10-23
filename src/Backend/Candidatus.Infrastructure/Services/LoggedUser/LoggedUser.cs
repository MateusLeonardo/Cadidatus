using Candidatus.Domain.Entities;
using Candidatus.Domain.Security.Tokens;
using Candidatus.Domain.Services.LoggedUser;
using Candidatus.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Candidatus.Infrastructure.Services.LoggedUser;

public class LoggedUser : ILoggedUser
{
    private readonly CandidatusDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(CandidatusDbContext dbContext, ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }

    public async Task<User> User()
    {
        var token = _tokenProvider.Value();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var userIdentifier = Guid.Parse(jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value);

        return await _dbContext.Users.AsNoTracking().FirstAsync(u => u.UserIdentifier == userIdentifier);
    }
}
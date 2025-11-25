using Candidatus.Domain.Repositories.Platform;
using Microsoft.EntityFrameworkCore;

namespace Candidatus.Infrastructure.DataAccess.Repositories;

public class PlatformRepository : IPlatformWriteOnlyRepository, IPlatformReadOnlyRepository
{
    private readonly CandidatusDbContext _dbContext;

    public PlatformRepository(CandidatusDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Domain.Entities.Platform platform) => await _dbContext.Platforms.AddAsync(platform);

    public async Task<IList<Domain.Entities.Platform>> FindAll(Domain.Entities.User user) =>
        await _dbContext.Platforms.AsNoTracking().Where(p => p.UserId == user.Id).ToListAsync();
}
using Candidatus.Domain.Repositories.Platform;

namespace Candidatus.Infrastructure.DataAccess.Repositories;

public class PlatformRepository : IPlatformWriteOnlyRepository
{
    private readonly CandidatusDbContext _dbContext;

    public PlatformRepository(CandidatusDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Domain.Entities.Platform platform) => await _dbContext.Platforms.AddAsync(platform);
}
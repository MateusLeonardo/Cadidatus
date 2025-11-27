using Candidatus.Domain.Repositories.Platform;
using Microsoft.EntityFrameworkCore;

namespace Candidatus.Infrastructure.DataAccess.Repositories;

public class PlatformRepository : IPlatformWriteOnlyRepository, IPlatformReadOnlyRepository, IPlatformUpdateOnlyRepository
{
    private readonly CandidatusDbContext _dbContext;

    public PlatformRepository(CandidatusDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Domain.Entities.Platform platform) => await _dbContext.Platforms.AddAsync(platform);

    public async Task<IList<Domain.Entities.Platform>> FindAll(Domain.Entities.User user) =>
        await _dbContext.Platforms.AsNoTracking().Where(p => p.UserId == user.Id).ToListAsync();

    async Task<Domain.Entities.Platform?> IPlatformUpdateOnlyRepository.FindById(int id, Domain.Entities.User user) =>
        await _dbContext.Platforms.FirstOrDefaultAsync(p => p.Id == id && p.UserId == user.Id);

    async Task<Domain.Entities.Platform?> IPlatformReadOnlyRepository.FindById(int id, Domain.Entities.User user) =>
        await _dbContext.Platforms.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id && p.UserId == user.Id);

    public void Update(Domain.Entities.Platform platform) => _dbContext.Platforms.Update(platform);

    public async Task Delete(int id)
    {
        var platform = await _dbContext.Platforms.FindAsync(id);

        _dbContext.Platforms.Remove(platform!);
    }
}
using Candidatus.Domain.Entities;
using Candidatus.Domain.Repositories.Application;
using Microsoft.EntityFrameworkCore;

namespace Candidatus.Infrastructure.DataAccess.Repositories;

public class ApplicationRepository : IApplicationWriteOnlyRepository, IApplicationReadOnlyRepository
{
    private readonly CandidatusDbContext _dbContext;

    public ApplicationRepository(CandidatusDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Application application) => await _dbContext.Applications.AddAsync(application);

    public async Task<IList<Application>> FindAll(User user) => await _dbContext.Applications
        .AsNoTracking().Where(p => p.UserId == user.Id).ToListAsync();
}

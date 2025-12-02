using Candidatus.Domain.Entities;
using Candidatus.Domain.Repositories.Application;

namespace Candidatus.Infrastructure.DataAccess.Repositories;

public class ApplicationRepository : IApplicationWriteOnlyRepository
{
    private readonly CandidatusDbContext _dbContext;

    public ApplicationRepository(CandidatusDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Application application) => await _dbContext.Applications.AddAsync(application);
}

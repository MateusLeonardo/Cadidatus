using Candidatus.Domain.Entities;
using Candidatus.Domain.Repositories.Company;

namespace Candidatus.Infrastructure.DataAccess.Repositories;
public class CompanyRepository : ICompanyWriteOnlyRepository
{
    private readonly CandidatusDbContext _dbContext;

    public CompanyRepository(CandidatusDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Company company) => await _dbContext.Companies.AddAsync(company);
}

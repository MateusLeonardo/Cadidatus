using Candidatus.Domain.Entities;
using Candidatus.Domain.Repositories.Company;
using Microsoft.EntityFrameworkCore;

namespace Candidatus.Infrastructure.DataAccess.Repositories;
public class CompanyRepository : ICompanyWriteOnlyRepository, ICompanyReadOnlyRepository, ICompanyUpdateOnlyRepository
{
    private readonly CandidatusDbContext _dbContext;

    public CompanyRepository(CandidatusDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Company company) => await _dbContext.Companies.AddAsync(company);

    public async Task<IList<Company>> FindAll(User user) => await _dbContext.Companies
        .AsNoTracking().Where(c => c.UserId == user.Id).Include(c => c.City).ThenInclude(c => c.State).ToListAsync();


    async Task<Company?> ICompanyUpdateOnlyRepository.FindById(int id, User user) => await _dbContext.Companies
        .FirstOrDefaultAsync(c => c.Id == id && c.UserId == user.Id);

    async Task<Company?> ICompanyReadOnlyRepository.FindById(int id, User user) => await _dbContext.Companies.AsNoTracking()
        .FirstOrDefaultAsync(c => c.Id == id && c.UserId == user.Id);

    public void Update(Company company) => _dbContext.Companies.Update(company);

    public async Task Delete(int id)
    {
        var company = await _dbContext.Companies.FindAsync(id);

        _dbContext.Companies.Remove(company!);
    }

    public async Task<bool> Exists(int id, User user) => await _dbContext.Companies
        .AsNoTracking().AnyAsync(c => c.Id == id && c.UserId == user.Id);
}

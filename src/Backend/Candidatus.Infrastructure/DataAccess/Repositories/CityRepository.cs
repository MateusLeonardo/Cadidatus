using Candidatus.Domain.Entities;
using Candidatus.Domain.Repositories.City;
using Microsoft.EntityFrameworkCore;

namespace Candidatus.Infrastructure.DataAccess.Repositories;

public class CityRepository : ICityWriteOnlyRepository, ICityReadOnlyRepository, ICityUpdateOnlyRepository
{
    private readonly CandidatusDbContext _dbContext;
    public CityRepository(CandidatusDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(City city) => await _dbContext.Cities.AddAsync(city);

    public async Task<IList<City>> FindAll(User user) => await _dbContext.Cities
        .AsNoTracking().Where(c => c.UserId == user.Id).OrderBy(c => c.Name).Include(c => c.State).ToListAsync();

    async Task<City?> ICityUpdateOnlyRepository.FindById(int id, User user) 
        => await _dbContext.Cities.FirstOrDefaultAsync(c => c.Id == id && c.UserId == user.Id);

    async Task<City?> ICityReadOnlyRepository.FindById(int id, User user) 
        => await _dbContext.Cities.AsNoTracking().Include(c => c.State).FirstOrDefaultAsync(c => c.Id == id && c.UserId == user.Id);

    public void Update(City city) => _dbContext.Cities.Update(city);
}

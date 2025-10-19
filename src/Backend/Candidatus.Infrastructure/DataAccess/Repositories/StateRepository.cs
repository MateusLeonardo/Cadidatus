using Candidatus.Domain.Entities;
using Candidatus.Domain.Repositories.State;
using Microsoft.EntityFrameworkCore;

namespace Candidatus.Infrastructure.DataAccess.Repositories;
public class StateRepository : IStateWriteOnlyRepository, IStateReadOnlyRepository
{
    private readonly CandidatusDbContext _dbContext;

    public StateRepository(CandidatusDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(State state) => await _dbContext.States.AddAsync(state);

    public async Task<bool> ExistsWithUf(Domain.Entities.User user, string uf) =>
        await _dbContext.States.AnyAsync(s => s.UserId == user.Id && s.Uf == uf);

    public async Task<IList<State>> FindAll(Domain.Entities.User user) =>
        await _dbContext.States.AsNoTracking().Where(s => s.UserId == user.Id).ToListAsync();
}

using Candidatus.Domain.Entities;
using Candidatus.Domain.Repositories.State;
using Microsoft.EntityFrameworkCore;

namespace Candidatus.Infrastructure.DataAccess.Repositories;

public class StateRepository : IStateWriteOnlyRepository, IStateReadOnlyRepository, IStateUpdateOnlyRepository
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
        await _dbContext.States.AsNoTracking().Where(s => s.UserId == user.Id).OrderBy(x => x.Name).ToListAsync();

    async Task<State?> IStateUpdateOnlyRepository.FindById(int id, Domain.Entities.User user) =>
        await _dbContext.States.FirstOrDefaultAsync(s => s.Id == id && s.UserId == user.Id);

    async Task<State?> IStateReadOnlyRepository.FindById(int id, Domain.Entities.User user) =>
        await _dbContext.States.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id && s.UserId == user.Id);

    public void Update(State state) => _dbContext.States.Update(state);

    public async Task Delete(int id)
    {
        var state = await _dbContext.States.FindAsync(id);

        _dbContext.States.Remove(state!);
    }
    public async Task<bool> ExistsWithUfExceptId(Domain.Entities.User user, string uf, int exceptId)
    {
        return await _dbContext.States
            .AnyAsync(state => state.UserId == user.Id
                            && state.Uf == uf
                            && state.Id != exceptId);
    }
}

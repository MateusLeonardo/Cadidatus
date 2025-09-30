using Candidatus.Domain.Repositories;

namespace Candidatus.Infrastructure.DataAccess;
public class UnitOfWork : IUnitOfWork
{
    private readonly CandidatusDbContext _context;
    public UnitOfWork(CandidatusDbContext context)
    {
        _context = context;
    }
    public async Task CommitAsync() => await _context.SaveChangesAsync();
}

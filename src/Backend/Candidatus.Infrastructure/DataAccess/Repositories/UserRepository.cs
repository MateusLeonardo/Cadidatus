using Candidatus.Domain.Entities;
using Candidatus.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace Candidatus.Infrastructure.DataAccess.Repositories;
public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
{
    private readonly CandidatusDbContext _dbContext;
    public UserRepository(CandidatusDbContext dbContext) => _dbContext = dbContext;
    public async Task Add(Domain.Entities.User user) => await _dbContext.Users.AddAsync(user);

    public Task<User?> GetByEmail(string email) => _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
}

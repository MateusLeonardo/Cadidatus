using Candidatus.Domain.Entities;
using Candidatus.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace Candidatus.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository, IUserUpdateOnlyRepository
{
    private readonly CandidatusDbContext _dbContext;

    public UserRepository(CandidatusDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> ExistUserWithEmail(string email)
    {
        return await _dbContext.Users.AnyAsync(u => u.Email == email);
    }

    public async Task Add(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }

    public async Task<User?> GetById(int id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<bool> ExistUserWithIdentifier(Guid userIdentifier)
    {
        return await _dbContext.Users.AnyAsync(user => user.UserIdentifier.Equals(userIdentifier));
    }

    public void Update(User user)
    {
        _dbContext.Users.Update(user);
    }
}
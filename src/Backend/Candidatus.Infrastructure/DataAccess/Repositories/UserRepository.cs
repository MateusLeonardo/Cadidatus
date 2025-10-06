using Candidatus.Domain.Entities;
using Candidatus.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace Candidatus.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
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

    public async Task Add(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }

    public async Task<User?> GetById(int id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
    }
}
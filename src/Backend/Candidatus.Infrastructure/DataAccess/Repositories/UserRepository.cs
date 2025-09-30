using Candidatus.Domain.Entities;
using Candidatus.Domain.Repositories.User;

namespace Candidatus.Infrastructure.DataAccess.Repositories;
public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
{

    public async Task Add(Domain.Entities.User user) =>

    public Task<User?> GetByEmail(string email)
    {
        throw new NotImplementedException();
    }
}

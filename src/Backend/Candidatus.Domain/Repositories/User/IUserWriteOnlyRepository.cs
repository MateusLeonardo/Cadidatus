namespace Candidatus.Domain.Repositories.User;

public interface IUserWriteOnlyRepository
{
    public Task Add(Entities.User user);
    public Task<Entities.User?> GetById(int id);
}
namespace Candidatus.Domain.Repositories.User;

public interface IUserReadOnlyRepository
{
    public Task<Entities.User?> GetByEmail(string email);
    public Task<bool> ExistUserWithEmail(string email);
}
namespace Candidatus.Domain.Repositories.User;
public interface IUserUpdateOnlyRepository
{
    Task<Entities.User> GetById(int id);

    void Update(Entities.User user);
}

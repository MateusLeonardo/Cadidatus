namespace Candidatus.Domain.Repositories.Application;

public interface IApplicationUpdateOnlyRepository
{
    void Update(Entities.Application application);
    Task<Entities.Application?> FindById(int id, Entities.User user);
}

namespace Candidatus.Domain.Repositories.City;

public interface ICityUpdateOnlyRepository
{
    Task<Entities.City?> FindById(int id, Entities.User user);
    void Update(Entities.City city);
}
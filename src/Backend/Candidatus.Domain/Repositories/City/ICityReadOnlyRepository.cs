namespace Candidatus.Domain.Repositories.City;

public interface ICityReadOnlyRepository {

    Task<IList<Entities.City>> FindAll(Entities.User user);
}
namespace Candidatus.Domain.Repositories.City;

public interface ICityWriteOnlyRepository
{
    Task Add(Entities.City city);
}

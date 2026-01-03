namespace Candidatus.Domain.Repositories.Application;

public interface IApplicationWriteOnlyRepository
{
    Task Add(Entities.Application application);
    Task Delete(int id);
}

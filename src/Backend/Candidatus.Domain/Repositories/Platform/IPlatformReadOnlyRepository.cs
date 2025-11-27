namespace Candidatus.Domain.Repositories.Platform;

public interface IPlatformReadOnlyRepository
{
    Task<IList<Entities.Platform>> FindAll(Entities.User user);
    Task<Entities.Platform?> FindById(int id, Entities.User user);
}
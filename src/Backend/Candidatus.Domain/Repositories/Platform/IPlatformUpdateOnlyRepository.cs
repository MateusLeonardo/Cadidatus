namespace Candidatus.Domain.Repositories.Platform;

public interface IPlatformUpdateOnlyRepository
{
    Task<Entities.Platform?> FindById(int id, Entities.User user);
    void Update(Entities.Platform platform);
}
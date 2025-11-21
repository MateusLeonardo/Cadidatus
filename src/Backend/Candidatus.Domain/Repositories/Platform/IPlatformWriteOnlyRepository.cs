namespace Candidatus.Domain.Repositories.Platform;

public interface IPlatformWriteOnlyRepository
{
    Task Add(Entities.Platform platform);
}
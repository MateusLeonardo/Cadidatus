using Candidatus.Domain.Repositories.Platform;
using Moq;

namespace CommonTestUtilities.Repositories.Platform;

public class PlatformReadOnlyRepositoryBuilder
{
    private readonly Mock<IPlatformReadOnlyRepository> _repository = new();

    public PlatformReadOnlyRepositoryBuilder FindById(Candidatus.Domain.Entities.User user, Candidatus.Domain.Entities.Platform platform)
    {
        _repository.Setup(repo => repo.FindById(platform.Id, user)).ReturnsAsync(platform);
        return this;
    }

    public IPlatformReadOnlyRepository Build() => _repository.Object;
}
using Candidatus.Domain.Repositories.Platform;
using Moq;

namespace CommonTestUtilities.Repositories.Platform;

public class PlatformUpdateOnlyRepositoryBuilder
{
    private readonly Mock<IPlatformUpdateOnlyRepository> _repository = new();

    public PlatformUpdateOnlyRepositoryBuilder FindById(Candidatus.Domain.Entities.User user, Candidatus.Domain.Entities.Platform platform)
    {
        _repository.Setup(repo => repo.FindById(platform.Id, user)).ReturnsAsync(platform);
        return this;
    }

    public PlatformUpdateOnlyRepositoryBuilder Update(Candidatus.Domain.Entities.Platform platform)
    {
        _repository.Setup(repo => repo.Update(platform));
        return this;
    }

    public IPlatformUpdateOnlyRepository Build() => _repository.Object;
}
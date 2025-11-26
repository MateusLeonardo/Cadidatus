using Candidatus.Domain.Repositories.Platform;
using Moq;

namespace CommonTestUtilities.Repositories.Platform;

public class PlatformWriteOnlyRepositoryBuilder
{
    public static IPlatformWriteOnlyRepository Build()
    {
        var mock = new Mock<IPlatformWriteOnlyRepository>();
        return mock.Object;
    }
}
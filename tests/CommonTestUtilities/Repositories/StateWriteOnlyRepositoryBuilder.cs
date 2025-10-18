using Candidatus.Domain.Repositories.State;
using Moq;

namespace CommonTestUtilities.Repositories;
public class StateWriteOnlyRepositoryBuilder
{
    public static IStateWriteOnlyRepository Build()
    {
        var mock = new Mock<IStateWriteOnlyRepository>();
        return mock.Object;
    }
}

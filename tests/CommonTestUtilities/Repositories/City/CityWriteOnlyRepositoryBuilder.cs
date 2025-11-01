using Candidatus.Domain.Repositories.City;
using Moq;

namespace CommonTestUtilities.Repositories.City;

public class CityWriteOnlyRepositoryBuilder
{
    public static ICityWriteOnlyRepository Build()
    {
        var mock = new Mock<ICityWriteOnlyRepository>();
        return mock.Object;
    }
}
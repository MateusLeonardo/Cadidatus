using Candidatus.Domain.Repositories.Company;
using Moq;

namespace CommonTestUtilities.Repositories.Company;

public class CompanyWriteOnlyRepositoryBuilder
{
    public static ICompanyWriteOnlyRepository Build()
    {
        var mock = new Mock<ICompanyWriteOnlyRepository>();
        return mock.Object;
    }
}
using Candidatus.Domain.Repositories.Company;
using Moq;

namespace CommonTestUtilities.Repositories.Company;

public class CompanyWriteOnlyRepositoryBuilder
{
    private readonly Mock<ICompanyWriteOnlyRepository> _repository = new();

    public CompanyWriteOnlyRepositoryBuilder Delete(Candidatus.Domain.Entities.Company company)
    {
        _repository.Setup(repo => repo.Delete(company.Id)).Returns(Task.CompletedTask);
        return this;
    }

    public ICompanyWriteOnlyRepository Build() => _repository.Object;
}
using Candidatus.Domain.Repositories.Company;
using Moq;

namespace CommonTestUtilities.Repositories.Company;

public class CompanyReadOnlyRepositoryBuilder
{
    private readonly Mock<ICompanyReadOnlyRepository> _repository = new();

    public void FindById(Candidatus.Domain.Entities.Company company, Candidatus.Domain.Entities.User user)
    {
        _repository.Setup(repo => repo.FindById(company.Id, user)).ReturnsAsync(company);
    }

    public ICompanyReadOnlyRepository Build() => _repository.Object;
}
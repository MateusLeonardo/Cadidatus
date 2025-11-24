using Candidatus.Domain.Repositories.Company;
using Moq;

namespace CommonTestUtilities.Repositories.Company;

public class CompanyUpdateOnlyRepositoryBuilder
{
    private readonly Mock<ICompanyUpdateOnlyRepository> _repository = new();


    public CompanyUpdateOnlyRepositoryBuilder FindById(Candidatus.Domain.Entities.User user, Candidatus.Domain.Entities.Company company)
    {
        _repository.Setup(repo => repo.FindById(company.Id, user)).ReturnsAsync(company);
        return this;
    }

    public CompanyUpdateOnlyRepositoryBuilder Update(Candidatus.Domain.Entities.Company company)
    {
        _repository.Setup(repo => repo.Update(company));
        return this;
    }

    public ICompanyUpdateOnlyRepository Build() => _repository.Object;
}
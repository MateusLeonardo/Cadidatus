using Candidatus.Application.UseCases.Company.Delete;
using Candidatus.Exceptions;
using Candidatus.Exceptions.ExceptionsBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.Company;
using FluentAssertions;

namespace UseCases.Test.Company.Delete;

public class DeleteCompanyUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var (user, state, city) = BuildUserStateAndCity();
        var company = CompanyBuilder.Build(user, city);

        var useCase = CreateUseCase(user, company);

        var act = async () => await useCase.Execute(company.Id);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Error_Company_Not_Found()
    {
        var (user, state, city) = BuildUserStateAndCity();
        var company = CompanyBuilder.Build(user, city);

        var useCase = CreateUseCase(user, company);

        var act = async () => await useCase.Execute(99);

        await act.Should().ThrowAsync<NotFoundException>()
            .Where(err => err.GetErrorMessages().Count == 1 && err.GetErrorMessages().Contains(ResourceMessagesExceptions.COMPANY_NOT_FOUND));
    }

    private static DeleteCompanyUseCase CreateUseCase(
        Candidatus.Domain.Entities.User user, 
        Candidatus.Domain.Entities.Company? company = null)
    {
        var deleteOnlyRepository = new CompanyWriteOnlyRepositoryBuilder();
        var readOnlyRepository = new CompanyReadOnlyRepositoryBuilder();

        if(company is not null) {
            readOnlyRepository.FindById(company, user);
            deleteOnlyRepository.Delete(company);
        }

        var unitOfWork = UnitOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        return new DeleteCompanyUseCase(loggedUser, readOnlyRepository.Build(), deleteOnlyRepository.Build(), unitOfWork);
    }


    private static (
        Candidatus.Domain.Entities.User user, 
        Candidatus.Domain.Entities.State state, 
        Candidatus.Domain.Entities.City city) BuildUserStateAndCity()
    {
        var (user, _) = UserBuilder.Build();
        var state = StateBuilder.Build(user);
        var city = CityBuilder.Build(user, state);
        return (user, state, city);
    }
}
using Candidatus.Application.UseCases.Company.Update;
using Candidatus.Exceptions;
using Candidatus.Exceptions.ExceptionsBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.City;
using CommonTestUtilities.Repositories.Company;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace UseCases.Test.Company.Update;

public class UpdateCompanyUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var (user, state, city) = BuildUserStateAndCity();
        var request = RequestUpdateCompanyJsonBuilder.Build(city.Id);
        var company = CompanyBuilder.Build(user, city);

        var useCase = CreateUseCase(user, company, city);

        var act = async () => await useCase.Execute(company.Id, request);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Error_Company_Name_Empty()
    {
        var (user, state, city) = BuildUserStateAndCity();
        var request = RequestUpdateCompanyJsonBuilder.Build(city.Id);
        request.Name = string.Empty;
        var company = CompanyBuilder.Build(user, city);

        var useCase = CreateUseCase(user, company, city);

        var act = async () => await useCase.Execute(company.Id, request);

        await act.Should().ThrowAsync<ErrorOnValidationException>()
            .Where(err => err.GetErrorMessages().Count == 1 && err.GetErrorMessages().Contains(ResourceMessagesExceptions.COMPANY_EMPTY));
    }

    [Fact]
    public async Task Error_City_Not_Found()
    {
        var (user, state, city) = BuildUserStateAndCity();
        var request = RequestUpdateCompanyJsonBuilder.Build(99);
        var company = CompanyBuilder.Build(user, city);
        

        var useCase = CreateUseCase(user, company, city);

        var act = async () => await useCase.Execute(company.Id, request);

        await act.Should().ThrowAsync<NotFoundException>()
            .Where(err => err.GetErrorMessages().Count == 1 && err.GetErrorMessages().Contains(ResourceMessagesExceptions.CITY_NOT_FOUND));
    }

    private static UpdateCompanyUseCase CreateUseCase(
        Candidatus.Domain.Entities.User user,
        Candidatus.Domain.Entities.Company? company = null,
        Candidatus.Domain.Entities.City? city = null)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var cityReadOnlyRepository = new CityReadOnlyRepositoryBuilder();
        if (city is not null)
            cityReadOnlyRepository.FindById(city, user);

        var updateOnlyRepository = new CompanyUpdateOnlyRepositoryBuilder();
        if (company is not null)
        {
            updateOnlyRepository
                .FindById(user, company)
                .Update(company);
        }

        var unitOfWork = UnitOfWorkBuilder.Build();
        return new UpdateCompanyUseCase(updateOnlyRepository.Build(), unitOfWork, loggedUser, cityReadOnlyRepository.Build());
    }

    private static (Candidatus.Domain.Entities.User user, Candidatus.Domain.Entities.State state, Candidatus.Domain.Entities.City city) BuildUserStateAndCity()
    {
        var (user, _) = UserBuilder.Build();
        var state = StateBuilder.Build(user);
        var city = CityBuilder.Build(user, state);
        return (user, state, city);
    }
}
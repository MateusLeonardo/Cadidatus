using Candidatus.Application.UseCases.Company.Register;
using Candidatus.Exceptions;
using Candidatus.Exceptions.ExceptionsBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.City;
using CommonTestUtilities.Repositories.Company;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Tests.UseCases.Test.Company.Register;

public class RegisterCompanyUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var (user, _) = UserBuilder.Build();
        var state = StateBuilder.Build(user);
        var city = CityBuilder.Build(user, state);
        var request = RequestRegisterCompanyJsonBuilder.Build(city.Id);

        var useCase = CreateUseCase(user, city);

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Name.Should().Be(request.Name);
    }

    [Fact]
    public async Task Erro_Company_Name_Empty()
    {
        var (user, _) = UserBuilder.Build();
        var state = StateBuilder.Build(user);
        var city = CityBuilder.Build(user, state);
        var request = RequestRegisterCompanyJsonBuilder.Build(city.Id);
        request.Name = string.Empty;

        var useCase = CreateUseCase(user, city);

        var act = async () => await useCase.Execute(request);

        await act.Should().ThrowAsync<ErrorOnValidationException>()
            .Where(e => e.GetErrorMessages().Count == 1 && e.GetErrorMessages().Contains(ResourceMessagesExceptions.COMPANY_EMPTY));
    }

    [Fact]
    public async Task Error_City_Not_Found()
    {
        var (user, _) = UserBuilder.Build();
        var request = RequestRegisterCompanyJsonBuilder.Build(99);

        var useCase = CreateUseCase(user);

        var act = async () => await useCase.Execute(request);

        await act.Should().ThrowAsync<NotFoundException>()
            .Where(err => err.GetErrorMessages().Count == 1 && err.GetErrorMessages().Contains(ResourceMessagesExceptions.CITY_NOT_FOUND));
    }

    private static RegisterCompanyUseCase CreateUseCase(Candidatus.Domain.Entities.User user, Candidatus.Domain.Entities.City? city = null)
    {
        var companyWriteOnlyRepository = CompanyWriteOnlyRepositoryBuilder.Build();
        var cityReadOnlyRepository = new CityReadOnlyRepositoryBuilder();

        if (city is not null)
            cityReadOnlyRepository.FindById(city, user);

        var loggedUser = LoggedUserBuilder.Build(user);
        var unitOfWork = UnitOfWorkBuilder.Build();
        var mapper = MapperBuilder.Build();

        return new RegisterCompanyUseCase(companyWriteOnlyRepository, loggedUser, cityReadOnlyRepository.Build(), unitOfWork, mapper);
    }
}
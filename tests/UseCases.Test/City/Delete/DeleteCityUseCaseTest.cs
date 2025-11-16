using System;
using Candidatus.Application.UseCases.City.Delete;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.City;
using FluentAssertions;

namespace UseCases.Test.City.Delete;

public class DeleteCityUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var (user, _) = UserBuilder.Build();
        var state = StateBuilder.Build(user);
        var city = CityBuilder.Build(user, state);

        var useCase = CreateUseCase(user, city);

        var act = async () => await useCase.Execute(city.Id);

        await act.Should().NotThrowAsync();
    }

    private static DeleteCityUseCase CreateUseCase(Candidatus.Domain.Entities.User user, Candidatus.Domain.Entities.City? city = null)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var cityWriteOnlyRepository = CityWriteOnlyRepositoryBuilder.Build();
        var cityReadOnlyRepository = new CityReadOnlyRepositoryBuilder();
        var unitOfWork = UnitOfWorkBuilder.Build();

        if (city is not null)
            cityReadOnlyRepository.FindById(city, user);

        return new DeleteCityUseCase(loggedUser, cityWriteOnlyRepository, cityReadOnlyRepository.Build(), unitOfWork);
    }
}

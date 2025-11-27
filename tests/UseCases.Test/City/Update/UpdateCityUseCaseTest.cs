using Candidatus.Application.UseCases.City.Update;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.City;
using CommonTestUtilities.Repositories.State;
using CommonTestUtilities.Requests;
using FluentAssertions;
using Candidatus.Exceptions;
using Candidatus.Exceptions.ExceptionsBase;

namespace UseCases.Test.City.Update;

public class UpdateCityUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var (user, _) = UserBuilder.Build();
        var state = StateBuilder.Build(user);
        var city = CityBuilder.Build(user, state);
        var request = RequestUpdateCityJsonBuilder.Build(state.Id);

        var useCase = CreateUseCase(user, city, state);

        var act = async () => await useCase.Execute(city.Id, request);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Error_City_Not_Found()
    {
        var (user, _) = UserBuilder.Build();
        var state = StateBuilder.Build(user);
        var city = CityBuilder.Build(user, state);
        var request = RequestUpdateCityJsonBuilder.Build(state.Id);
        
        var useCase = CreateUseCase(user, city, state);

        city.Id = 999;
        var act = async () => await useCase.Execute(city.Id, request);

        await act.Should().ThrowAsync<NotFoundException>()
            .Where(ex => ex.GetErrorMessages().Count == 1 && ex.GetErrorMessages().Contains(ResourceMessagesExceptions.CITY_NOT_FOUND));
    }

    [Fact]
    public async Task Error_State_Not_Found()
    {
        var request = RequestUpdateCityJsonBuilder.Build(2);
        request.StateId = 999;
        var (user, _) = UserBuilder.Build();
        var state = StateBuilder.Build(user);
        var city = CityBuilder.Build(user, state);

        var useCase = CreateUseCase(user, city, state);

        var act = async () => await useCase.Execute(city.Id, request);

        await act.Should().ThrowAsync<NotFoundException>()
            .Where(ex => ex.GetErrorMessages().Count == 1 && ex.GetErrorMessages().Contains(ResourceMessagesExceptions.STATE_NOT_FOUND));
    }

    private static UpdateCityUseCase CreateUseCase(
        Candidatus.Domain.Entities.User user,
        Candidatus.Domain.Entities.City? city = null,
        Candidatus.Domain.Entities.State? state = null)
    {
        var cityUpdateOnlyRepository = new CityUpdateOnlyRepositoryBuilder();
        if (city is not null)
        {
            cityUpdateOnlyRepository
                .FindById(user, city)
                .Update(city);
        }

        var stateReadOnlyRepository = new StateReadOnlyRepositoryBuilder();
        if (state is not null)
            stateReadOnlyRepository.FindById(state.Id, user, state);

        var unitOfWork = UnitOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new UpdateCityUseCase(cityUpdateOnlyRepository.Build(), stateReadOnlyRepository.Build(), unitOfWork, loggedUser);
    }
}
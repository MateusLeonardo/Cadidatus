using Candidatus.Application.UseCases.City.Register;
using Candidatus.Exceptions;
using Candidatus.Exceptions.ExceptionsBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.City;
using CommonTestUtilities.Repositories.State;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace UseCases.Test.City.Register;

public class RegisterCityUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var (user, _) = UserBuilder.Build();
        var state = StateBuilder.Build(user);

        var request = RequestRegisterCityJsonBuilder.Build(state.Id);

        var useCase = CreateUseCase(user, state);

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
    }
    [Fact]
    public async Task Error_City_Name_Empty() {
        var request = RequestRegisterCityJsonBuilder.Build(1);
        request.Name = string.Empty;
        var (user, _) = UserBuilder.Build();
        var useCase = CreateUseCase(user);

        var act = async () => await useCase.Execute(request);

        (await act.Should().ThrowAsync<ErrorOnValidationException>()).Where(e =>
            e.GetErrorMessages().Count == 1 && e.GetErrorMessages().Contains(ResourceMessagesExceptions.CITY_EMPTY));
    }

    [Fact]
    public async Task Error_State_Not_Found() {
        var request = RequestRegisterCityJsonBuilder.Build(1);
        var (user, _) = UserBuilder.Build();

        var useCase = CreateUseCase(user);

        var act = async () => await useCase.Execute(request);

        await act.Should().ThrowAsync<NotFoundException>()
            .Where(ex => ex.GetErrorMessages().Count == 1 && ex.GetErrorMessages().Contains(ResourceMessagesExceptions.STATE_NOT_FOUND));
    }

    private static RegisterCityUseCase CreateUseCase(
        Candidatus.Domain.Entities.User user, 
        Candidatus.Domain.Entities.State? state = null)
    {
        var cityWriteOnlyRepository = CityWriteOnlyRepositoryBuilder.Build();
        var stateReadOnlyRepository = new StateReadOnlyRepositoryBuilder();

        if(state is not null)
            stateReadOnlyRepository.FindById(state.Id, user, state);

        var unitOfWork = UnitOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        var mapper = MapperBuilder.Build();

        return new RegisterCityUseCase(cityWriteOnlyRepository, stateReadOnlyRepository.Build(), unitOfWork, loggedUser, mapper);
    }
}
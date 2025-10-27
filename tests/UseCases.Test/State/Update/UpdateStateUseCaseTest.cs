using Candidatus.Application.UseCases.State.Update;
using Candidatus.Exceptions;
using Candidatus.Exceptions.ExceptionsBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.State;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace UseCases.Test.State;

public class UpdateStateUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestUpdateStateJsonBuilder.Build();
        var (user, _) = UserBuilder.Build();
        var state = StateBuilder.Build(user);
        
        var usecase = CreateUseCase(user, state, request.Uf);

        var act = async () => await usecase.Execute(state.Id, request);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Success_Update_Keeping_Same_Uf()
    {
        var request = RequestUpdateStateJsonBuilder.Build();
        var (user, _) = UserBuilder.Build();
        var state = StateBuilder.Build(user);

        request.Uf = state.Uf; 

        var useCase = CreateUseCase(user, state, request.Uf, sameUfAsState: true);

        var act = async () => await useCase.Execute(state.Id, request);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Error_State_With_Same_Uf_Already_Exists()
    {
        var request = RequestUpdateStateJsonBuilder.Build();
        var (user, _) = UserBuilder.Build();
        var state = StateBuilder.Build(user);

        request.Uf = "PR"; 

        var useCase = CreateUseCase(user, state, request.Uf, existingUfInOtherState: true);

        var act = async () => await useCase.Execute(state.Id, request);

        await act.Should().ThrowAsync<ErrorOnValidationException>()
            .Where(ex => ex.GetErrorMessages().Contains(ResourceMessagesExceptions.UF_EXISTS));
    }

    [Fact]
    public async Task Error_State_Not_Found()
    {
        var request = RequestUpdateStateJsonBuilder.Build();
        var (user, _) = UserBuilder.Build();
        var state = StateBuilder.Build(user);

        var useCase = CreateUseCase(user, state, request.Uf);

        state.Id = 19;

        var act = async () => await useCase.Execute(state.Id, request);

        await act.Should().ThrowAsync<NotFoundException>()
            .Where(ex => ex.GetErrorMessages().Count == 1 && ex.GetErrorMessages()
                .Contains(ResourceMessagesExceptions.STATE_NOT_FOUND));
    }

    private static UpdateStateUseCase CreateUseCase
    (
        Candidatus.Domain.Entities.User user,
        Candidatus.Domain.Entities.State state,
        string requestUf,
        bool sameUfAsState = false,
        bool existingUfInOtherState = false
    )
    {
        var updateOnlyRepository = new StateUpdateOnlyRepositoryBuilder();
        var readOnlyRepository = new StateReadOnlyRepositoryBuilder();
        var loggedUser = LoggedUserBuilder.Build(user);
        var unitOfWork = UnitOfWorkBuilder.Build();

        updateOnlyRepository.FindById(user, state, state.Id);
        updateOnlyRepository.Update(state);

        if (sameUfAsState)
        {
            readOnlyRepository.ExistsWithUfExceptId(user, requestUf, state.Id, false);
        }
        else if (existingUfInOtherState)
        {
            readOnlyRepository.ExistsWithUfExceptId(user, requestUf, state.Id, true);
        }
        else
        {
            readOnlyRepository.ExistsWithUfExceptId(user, requestUf, state.Id, false);
        }

        return new UpdateStateUseCase
        (
            updateOnlyRepository.Build(),
            unitOfWork,
            loggedUser,
            readOnlyRepository.Build()
        );
    }
}
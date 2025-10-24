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
        
        var usecase = CreateUseCase(user, state);

        var act = async () => await usecase.Execute(state.Id, request);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Error_State_With_Same_Uf_Alread_Exists()
    {
        var request = RequestUpdateStateJsonBuilder.Build();
        var (user, _) = UserBuilder.Build();
        var state = StateBuilder.Build(user);

        request.Uf = state.Uf;

        var useCase = CreateUseCase(user, state, existingUf: request.Uf);

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

        var useCase = CreateUseCase(user, state);

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
        string? existingUf = null
    )
    {
        var updateOnlyRepository = new StateUpdateOnlyRepositoryBuilder();
        var readOnlyRepository = new StateReadOnlyRepositoryBuilder();
        var loggedUser = LoggedUserBuilder.Build(user);
        var unitOfWork = UnitOfWorkBuilder.Build();

        updateOnlyRepository.FindById(user, state, state.Id);
        updateOnlyRepository.Update(state);

        if (existingUf is not null)
            readOnlyRepository.ExistsWithUf(user, existingUf, true);

        return new UpdateStateUseCase
        (
            updateOnlyRepository.Build(),
            unitOfWork,
            loggedUser,
            readOnlyRepository.Build()
        );
    }
}

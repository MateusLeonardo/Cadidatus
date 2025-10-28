using System;
using Candidatus.Application.UseCases.State.Delete;
using Candidatus.Exceptions;
using Candidatus.Exceptions.ExceptionsBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.State;
using FluentAssertions;

namespace UseCases.Test.State.Delete;

public class DeleteStateUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var (user, _) = UserBuilder.Build();
        var state = StateBuilder.Build(user);

        var useCase = CreateUseCase(user, state);

        var act = async () => await useCase.Execute(state.Id);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Error_State_Id_Not_Found()
    {
        var (user, _) = UserBuilder.Build();
        var state = StateBuilder.Build(user);

        var useCase = CreateUseCase(user, null);

        var act = async () => await useCase.Execute(state.Id);

        await act.Should().ThrowAsync<NotFoundException>()
            .Where(err => err.GetErrorMessages().Contains(ResourceMessagesExceptions.STATE_NOT_FOUND));
    }

    private static DeleteStateUseCase CreateUseCase(
        Candidatus.Domain.Entities.User user,
        Candidatus.Domain.Entities.State? state
    )
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var readOnlyRepository = new StateReadOnlyRepositoryBuilder();
        var writeOnlyRepository = StateWriteOnlyRepositoryBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();

        if(state is not null)
            readOnlyRepository.FindById(state.Id, user, state);

        return new DeleteStateUseCase(
            loggedUser,
            readOnlyRepository.Build(),
            writeOnlyRepository,
            unitOfWork
        );
    }
}
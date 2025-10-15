using Candidatus.Application.UseCases.User.ChangePassword;
using Candidatus.Exceptions;
using Candidatus.Communication.Requests;
using Candidatus.Exceptions.ExceptionsBase;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace UseCases.Test.User.ChangePassword;

public class ChangePasswordUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestChangePasswordUserJsonBuilder.Build();
        var (user, password) = UserBuilder.Build();
        request.Password = password;

        var useCase = CreateUseCase(user);

        var act = async () => await useCase.Execute(request);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Error_NewPassword_Empty()
    {
        var (user, password) = UserBuilder.Build();
        var request = new RequestChangePasswordUserJson
        {
            Password = password,
            NewPassword = string.Empty
        };

        var useCase = CreateUseCase(user);

        var act = async () => await useCase.Execute(request);

        await act.Should().ThrowAsync<ErrorOnValidationException>().Where(e =>
            e.GetErrorMessages().Count == 1 && e.GetErrorMessages()
            .Contains(ResourceMessagesExceptions.PASSWORD_EMPTY));
    }

    [Fact]
    public async Task Error_Current_Password_Different()
    {
        var (user, _) = UserBuilder.Build();
        var request = RequestChangePasswordBuilder.Build();
        var useCase = CreateUseCase(user);

        var act = async () => await useCase.Execute(request);

        await act.Should().ThrowAsync<ErrorOnValidationException>().Where(e =>
            e.GetErrorMessages().Count == 1 && e.GetErrorMessages()
            .Contains(ResourceMessagesExceptions.PASSWORD_DIFFERENT_CURRENT_PASSWORD));
    }

    private static ChangePasswordUserUseCase CreateUseCase(Candidatus.Domain.Entities.User user)
    {
        var userUpdateOnlyRepository = new UserUpdateOnlyRepositoryBuilder().GetById(user).Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new ChangePasswordUserUseCase(userUpdateOnlyRepository, unitOfWork, passwordEncripter, loggedUser);
    }
}

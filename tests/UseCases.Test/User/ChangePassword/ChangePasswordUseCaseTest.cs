using Candidatus.Application.UseCases.User.ChangePassword;
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

    private static ChangePasswordUserUseCase CreateUseCase(Candidatus.Domain.Entities.User user)
    {
        var userUpdateOnlyRepository = new UserUpdateOnlyRepositoryBuilder().GetById(user).Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new ChangePasswordUserUseCase(userUpdateOnlyRepository, unitOfWork, passwordEncripter, loggedUser);
    }
}

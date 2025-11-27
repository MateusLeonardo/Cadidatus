using Candidatus.Application.UseCases.Login.DoLogin;
using Candidatus.Communication.Requests;
using Candidatus.Exceptions;
using Candidatus.Exceptions.ExceptionsBase;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using FluentAssertions;

namespace UseCases.Test.Login.DoLogin;

public class DoLoginUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var (user, password) = UserBuilder.Build();
        var useCase = CreateUseCase(user);

        var result = await useCase.Execute(new RequestLoginJson
        {
            Email = user.Email,
            Password = password
        });

        result.Should().NotBeNull();
        result.Tokens.AccessToken.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Error_Invalid_User()
    {
        var (user, password) = UserBuilder.Build();
        var request = RequestLoginJsonBuilder.Build();
        var useCase = CreateUseCase();

        var act = async () => await useCase.Execute(request);

        await act.Should().ThrowAsync<InvalidLoginException>()
            .Where(e => e.Message.Equals(ResourceMessagesExceptions.EMAIL_OR_PASSWORD_INVALID));
    }

    private static DoLoginUseCase CreateUseCase(Candidatus.Domain.Entities.User? user = null)
    {
        var accessToken = JwtTokenGeneratorBuilder.Buid();
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var mapper = MapperBuilder.Build();
        
        var readOnlyRepository = new UserReadOnlyRepositoryBuilder();
        if (user is not null)
            readOnlyRepository.GetByEmail(user);

        return new DoLoginUseCase(readOnlyRepository.Build(), accessToken, passwordEncripter, mapper);
    }
}

using Candidatus.Application.UseCases.User.Register;
using Candidatus.Exceptions;
using Candidatus.Exceptions.ExceptionsBase;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using FluentAssertions;

namespace UseCases.Test.User.Register;

public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateUseCase();

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Email.Should().Be(request.Email);
        result.Tokens.AccessToken.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Error_Email_Already_Registered()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateUseCase(request.Email);

        Func<Task> act = async () => await useCase.Execute(request);

        (await act.Should().ThrowAsync<ErrorOnValidationException>()).Where(e =>
            e.GetErrorMessages().Count == 1 && e.GetErrorMessages().Contains(ResourceMessagesExceptions.EMAIL_EXISTS));
    }

    [Fact]
    public async Task Error_Email_Empty()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = string.Empty;

        var useCase = CreateUseCase();

        Func<Task> act = async () => await useCase.Execute(request);

        (await act.Should().ThrowAsync<ErrorOnValidationException>()).Where(e =>
            e.GetErrorMessages().Count == 1 && e.GetErrorMessages().Contains(ResourceMessagesExceptions.EMAIL_EMPTY));
    }

    private static RegisterUserUseCase CreateUseCase(string? email = null)
    {
        var mapper = MapperBuilder.Build();
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var writeOnlyRepository = UserWriteOnlyRepositoryBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var readOnlyRepository = new UserReadOnlyRepositoryBuilder();
        var tokenGenerator = JwtTokenGeneratorBuilder.Buid();
        if (!string.IsNullOrEmpty(email))
            readOnlyRepository.ExistUserWithEmail(email);

        return new RegisterUserUseCase(writeOnlyRepository, readOnlyRepository.Build(), unitOfWork, mapper,
            passwordEncripter, tokenGenerator);
    }
}
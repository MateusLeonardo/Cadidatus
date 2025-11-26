using Candidatus.Application.UseCases.Platform.Register;
using Candidatus.Exceptions;
using Candidatus.Exceptions.ExceptionsBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.Platform;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Tests.UseCases.Test.Platform.Register;

public class RegisterPlatformUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterPlatformJsonBuilder.Build();
        var (user, _) = UserBuilder.Build();
        var useCase = CreateUseCase(user);
        
        var act = async () => await useCase.Execute(request);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Error_Platform_Name_Empty()
    {
        var request = RequestRegisterPlatformJsonBuilder.Build();
        request.Name = string.Empty;
        var (user, _) = UserBuilder.Build();
        var useCase = CreateUseCase(user);
        
        var act = async () => await useCase.Execute(request);

        await act.Should().ThrowAsync<ErrorOnValidationException>()
            .Where(err => err.GetErrorMessages().Count == 1 &&
                          err.GetErrorMessages().Contains(ResourceMessagesExceptions.PLATFORM_NAME_EMPTY));
    }

    [Fact]
    public async Task Error_Platform_Url_Empty()
    {
        var request = RequestRegisterPlatformJsonBuilder.Build();
        request.Url = string.Empty;
        var (user, _) = UserBuilder.Build();
        var useCase = CreateUseCase(user);
        
        var act = async () => await useCase.Execute(request);

        await act.Should().ThrowAsync<ErrorOnValidationException>()
            .Where(err => err.GetErrorMessages().Count == 1 &&
                          err.GetErrorMessages().Contains(ResourceMessagesExceptions.URL_EMPTY));
    }

    [Fact]
    public async Task Error_Platform_Url_Invalid()
    {
        var request = RequestRegisterPlatformJsonBuilder.Build();
        request.Url = "invalid-url";
        var (user, _) = UserBuilder.Build();
        var useCase = CreateUseCase(user);
        
        var act = async () => await useCase.Execute(request);

        await act.Should().ThrowAsync<ErrorOnValidationException>()
            .Where(err => err.GetErrorMessages().Count == 1 &&
                          err.GetErrorMessages().Contains(ResourceMessagesExceptions.URL_INVALID));
    }

    private static RegisterPlatformUseCase CreateUseCase(Candidatus.Domain.Entities.User user)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var mapper = MapperBuilder.Build();
        var writeOnlyRepository = PlatformWriteOnlyRepositoryBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();

        return new RegisterPlatformUseCase(writeOnlyRepository, unitOfWork, loggedUser, mapper);
    }
    
}
using Candidatus.Application.UseCases.Platform.Update;
using Candidatus.Exceptions;
using Candidatus.Exceptions.ExceptionsBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.Platform;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace UseCases.Test.Platform.Update;

public class UpdatePlatformUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestUpdatePlatformJsonBuilder.Build();
        var (user, _) = UserBuilder.Build();
        var platform = PlatformBuilder.Build(user);

        var useCase = CreateUseCase(user, platform);

        var act = async () => await useCase.Execute(platform.Id, request);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Error_Platform_Not_Found()
    {
        var request = RequestUpdatePlatformJsonBuilder.Build();
        var (user, _) = UserBuilder.Build();
        var platform = PlatformBuilder.Build(user);

        var useCase = CreateUseCase(user, platform);

        platform.Id = 1000;

        var act = async () => await useCase.Execute(platform.Id, request);

        await act.Should().ThrowAsync<NotFoundException>()
            .Where(ex => ex.GetErrorMessages().Count == 1 && ex.GetErrorMessages().Contains(ResourceMessagesExceptions.PLATFORM_NOT_FOUND));
    }

    [Fact]
    public async Task Error_Url_Invalid()
    {
        var request = RequestUpdatePlatformJsonBuilder.Build();
        var (user, _) = UserBuilder.Build();
        var platform = PlatformBuilder.Build(user);

        var useCase = CreateUseCase(user, platform);

        request.Url = "invalid-url";

        var act = async () => await useCase.Execute(platform.Id, request);

        await act.Should().ThrowAsync<ErrorOnValidationException>()
            .Where(ex => ex.GetErrorMessages().Count == 1 && ex.GetErrorMessages().Contains(ResourceMessagesExceptions.URL_INVALID));
    }

    private static UpdatePlatformUseCase CreateUseCase(
        Candidatus.Domain.Entities.User user,
        Candidatus.Domain.Entities.Platform? platform = null)
    {
        var updateOnlyRepository = new PlatformUpdateOnlyRepositoryBuilder();

        if (platform is not null)
        {
            updateOnlyRepository.FindById(user, platform);
            updateOnlyRepository.Update(platform);
        }

        var loggedUser = LoggedUserBuilder.Build(user);
        var unitOfWork = UnitOfWorkBuilder.Build();
        var mapper = MapperBuilder.Build();

        return new UpdatePlatformUseCase(updateOnlyRepository.Build(), unitOfWork, loggedUser, mapper);
    }
}
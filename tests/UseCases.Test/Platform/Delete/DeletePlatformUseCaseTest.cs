using Candidatus.Application.UseCases.Platform.Delete;
using Candidatus.Exceptions;
using Candidatus.Exceptions.ExceptionsBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.Platform;
using FluentAssertions;

namespace UseCases.Test.Platform.Delete;

public class DeletePlatformUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var (user, _) = UserBuilder.Build();
        var platform = PlatformBuilder.Build(user);

        var useCase = CreateUseCase(user, platform);

        var act = async () => await useCase.Execute(platform.Id);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Error_Platform_Not_Found()
    {
        var (user, _) = UserBuilder.Build();
        var platform = PlatformBuilder.Build(user);

        var useCase = CreateUseCase(user, null);

        var act = async () => await useCase.Execute(platform.Id);
        await act.Should().ThrowAsync<NotFoundException>()
            .Where(err => err.GetErrorMessages().Contains(ResourceMessagesExceptions.PLATFORM_NOT_FOUND));
    }

    private static DeletePlatformUseCase CreateUseCase(
        Candidatus.Domain.Entities.User user,
        Candidatus.Domain.Entities.Platform? platform = null)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var readOnlyRepository = new PlatformReadOnlyRepositoryBuilder();
        var writeOnlyRepository = PlatformWriteOnlyRepositoryBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();

        if(platform is not null)
            readOnlyRepository.FindById(user, platform);

        return new DeletePlatformUseCase(readOnlyRepository.Build(), writeOnlyRepository, unitOfWork, loggedUser);
    }
}
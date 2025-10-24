using Candidatus.Application.UseCases.State.Register;
using Candidatus.Exceptions;
using Candidatus.Exceptions.ExceptionsBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.State;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace UseCases.Test.State.Register;

public class RegisterStateUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var (user, _) = UserBuilder.Build();
        var request = RequestRegisterStateJsonBuilder.Build();

        var useCase = CreateUseCase(user: user, uf: request.Uf, existsUf: false);

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Name.Should().Be(request.Name);
    }

    [Fact]
    public async Task Error_Uf_Exists()
    {
        var request = RequestRegisterStateJsonBuilder.Build();
        var (user, _) = UserBuilder.Build();

        var useCase = CreateUseCase(user: user, uf: request.Uf, existsUf: true);

        var act = async () => await useCase.Execute(request);

        await act.Should().ThrowAsync<ErrorOnValidationException>()
            .Where(ex => ex.GetErrorMessages().Count == 1 && ex.GetErrorMessages().Contains(ResourceMessagesExceptions.UF_EXISTS));
    }
    public static RegisterStateUseCase CreateUseCase(Candidatus.Domain.Entities.User user, string uf, bool existsUf)
    {
        var readOnlyRepository = new StateReadOnlyRepositoryBuilder();
        readOnlyRepository.ExistsWithUf(user, uf, existsUf);
        var writeOnlyRepository = StateWriteOnlyRepositoryBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        var unitOfWork = UnitOfWorkBuilder.Build();
        var mapper = MapperBuilder.Build();

        return new RegisterStateUseCase(readOnlyRepository.Build(), writeOnlyRepository, loggedUser, unitOfWork, mapper);
    }
}

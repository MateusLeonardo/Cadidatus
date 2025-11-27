using Candidatus.Domain.Repositories.State;
using Moq;

namespace CommonTestUtilities.Repositories.State;

public class StateUpdateOnlyRepositoryBuilder
{
    private readonly Mock<IStateUpdateOnlyRepository> _repository = new();

    public StateUpdateOnlyRepositoryBuilder FindById(
        Candidatus.Domain.Entities.User user,
        Candidatus.Domain.Entities.State state,
        int id)
    {
        _repository.Setup(repo => repo.FindById(id, user)).ReturnsAsync(state);
        return this;
    }

    public StateUpdateOnlyRepositoryBuilder Update(Candidatus.Domain.Entities.State state)
    {
        _repository.Setup(repo => repo.Update(state));
        return this;
    }

    public IStateUpdateOnlyRepository Build() => _repository.Object;
}

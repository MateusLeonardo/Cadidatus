using Candidatus.Domain.Entities;
using Candidatus.Domain.Repositories.State;
using Moq;

namespace CommonTestUtilities.Repositories;
public class StateReadOnlyRepositoryBuilder
{
    private Mock<IStateReadOnlyRepository> _repository = new();

    public void ExistsWithUf(User user, string uf, bool existsUf)
    {
        _repository.Setup(repo => repo.ExistsWithUf(user, uf)).ReturnsAsync(existsUf);
    }

    public IStateReadOnlyRepository Build()
    {
        return _repository.Object;
    }
}

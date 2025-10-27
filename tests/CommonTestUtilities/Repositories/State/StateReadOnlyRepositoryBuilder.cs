using Candidatus.Domain.Repositories.State;
using Moq;

namespace CommonTestUtilities.Repositories.State;
public class StateReadOnlyRepositoryBuilder
{
    private Mock<IStateReadOnlyRepository> _repository = new();

    public void ExistsWithUf(Candidatus.Domain.Entities.User user, string uf, bool existsUf)
    {
        _repository.Setup(repo => repo.ExistsWithUf(user, uf)).ReturnsAsync(existsUf);
    }

     public void ExistsWithUfExceptId(Candidatus.Domain.Entities.User user, string uf, int exceptId, bool exists)
    {
        _repository.Setup(repo => repo.ExistsWithUfExceptId(user, uf, exceptId)).ReturnsAsync(exists);
    }

    public IStateReadOnlyRepository Build()
    {
        return _repository.Object;
    }
}

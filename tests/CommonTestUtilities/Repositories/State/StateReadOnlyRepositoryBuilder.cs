using Candidatus.Domain.Repositories.State;
using Moq;

namespace CommonTestUtilities.Repositories.State;
public class StateReadOnlyRepositoryBuilder
{
    private Mock<IStateReadOnlyRepository> _repository = new();

    public StateReadOnlyRepositoryBuilder ExistsWithUf(Candidatus.Domain.Entities.User user, string uf, bool existsUf)
    {
        _repository.Setup(repo => repo.ExistsWithUf(user, uf)).ReturnsAsync(existsUf);
        return this;
    }

    public StateReadOnlyRepositoryBuilder ExistsWithUfExceptId(Candidatus.Domain.Entities.User user, string uf, int exceptId, bool exists)
    {
        _repository.Setup(repo => repo.ExistsWithUfExceptId(user, uf, exceptId)).ReturnsAsync(exists);
        return this;
    }
    
    public StateReadOnlyRepositoryBuilder FindById(int id,Candidatus.Domain.Entities.User user ,Candidatus.Domain.Entities.State state)
    {
        _repository.Setup(repo => repo.FindById(id, user)).ReturnsAsync(state);
        return this;
    }

    public IStateReadOnlyRepository Build()
    {
        return _repository.Object;
    }
}

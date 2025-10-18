namespace Candidatus.Domain.Repositories.State;
public interface IStateWriteOnlyRepository
{
    public Task Add(Entities.State state);
}

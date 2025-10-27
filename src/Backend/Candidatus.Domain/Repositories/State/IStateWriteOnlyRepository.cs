namespace Candidatus.Domain.Repositories.State;
public interface IStateWriteOnlyRepository
{
    Task Add(Entities.State state);
    Task Delete(int id);

}

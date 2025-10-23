namespace Candidatus.Domain.Repositories.State;
public interface IStateUpdateOnlyRepository
{
    Task<Entities.State?> FindById(int id, Domain.Entities.User user);
    void Update(Entities.State state);
}

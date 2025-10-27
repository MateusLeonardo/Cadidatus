namespace Candidatus.Domain.Repositories.State;
public interface IStateReadOnlyRepository
{
    Task<bool> ExistsWithUf(Entities.User user, string uf);

    Task<IList<Entities.State>> FindAll(Entities.User user);

    Task<Entities.State?> FindById(int id, Entities.User user);
    Task<bool> ExistsWithUfExceptId(Domain.Entities.User user, string uf, int exceptId);
}

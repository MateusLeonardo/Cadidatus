namespace Candidatus.Domain.Repositories.State;
public interface IStateReadOnlyRepository
{
    public Task<bool> ExistsWithUf(Entities.User user, string uf);
}

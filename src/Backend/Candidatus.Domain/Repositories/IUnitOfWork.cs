namespace Candidatus.Domain.Repositories;
public interface IUnitOfWork
{
    public Task CommitAsync();
}

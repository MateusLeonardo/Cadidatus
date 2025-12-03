namespace Candidatus.Domain.Repositories.Application;

public interface IApplicationReadOnlyRepository
{
    Task<IList<Entities.Application>> FindAll(Entities.User user);
}

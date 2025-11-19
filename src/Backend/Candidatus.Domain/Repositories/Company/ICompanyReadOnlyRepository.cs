namespace Candidatus.Domain.Repositories.Company;

public interface ICompanyReadOnlyRepository
{
    Task<IList<Entities.Company>> FindAll(Entities.User user);
    Task<Entities.Company?> FindById(int id, Entities.User user);
}
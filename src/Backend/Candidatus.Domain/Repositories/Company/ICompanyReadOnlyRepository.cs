namespace Candidatus.Domain.Repositories.Company;

public interface ICompanyReadOnlyRepository
{
    Task<IList<Entities.Company>> FindAll(Entities.User user);
}
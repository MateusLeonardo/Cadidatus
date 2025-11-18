
namespace Candidatus.Domain.Repositories.Company;

public interface ICompanyUpdateOnlyRepository
{
    Task<Entities.Company?> FindById(int id, Entities.User user);
    void Update(Entities.Company company);
}
namespace Candidatus.Domain.Repositories.Company;
public interface ICompanyWriteOnlyRepository
{
    Task Add(Entities.Company company);
}

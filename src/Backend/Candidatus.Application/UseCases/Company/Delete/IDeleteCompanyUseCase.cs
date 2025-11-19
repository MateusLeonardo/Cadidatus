namespace Candidatus.Application.UseCases.Company.Delete;

public interface IDeleteCompanyUseCase
{
    Task Execute(int id);
}
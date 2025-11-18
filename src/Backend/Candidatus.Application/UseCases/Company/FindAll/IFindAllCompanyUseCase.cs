using Candidatus.Communication.Responses;

namespace Candidatus.Application.UseCases.Company.FindAll;

public interface IFindAllCompanyUseCase
{
    Task<ResponseAllCompanyJson> Execute();
}
using Candidatus.Communication.Requests;
using Candidatus.Communication.Responses;

namespace Candidatus.Application.UseCases.Company.Register;

public interface IRegisterCompanyUseCase
{
    Task<ResponseRegisteredCompanyJson> Execute(RequestRegisterCompanyJson request);
}

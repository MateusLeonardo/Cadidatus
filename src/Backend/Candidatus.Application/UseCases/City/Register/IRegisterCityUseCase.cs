using Candidatus.Communication.Requests;
using Candidatus.Communication.Responses;

namespace Candidatus.Application.UseCases.City.Register;

public interface IRegisterCityUseCase
{
    Task<ResponseRegisteredCityJson> Execute(RequestRegisterCityJson request);
}

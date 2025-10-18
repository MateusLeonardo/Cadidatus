using Candidatus.Communication.Requests;
using Candidatus.Communication.Responses;

namespace Candidatus.Application.UseCases.State.Register;
public interface IRegisterStateUseCase
{
    public Task<ResponseRegisteredStateJson> Execute(RequestRegisterStateJson request);
}

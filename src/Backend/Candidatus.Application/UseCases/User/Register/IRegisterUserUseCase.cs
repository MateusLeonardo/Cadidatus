using Candidatus.Communication.Requests;
using Candidatus.Communication.Responses;

namespace Candidatus.Application.UseCases.User.Register;
public interface IRegisterUserUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
}

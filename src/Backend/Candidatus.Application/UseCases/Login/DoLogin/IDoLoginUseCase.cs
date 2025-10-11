using Candidatus.Communication.Requests;
using Candidatus.Communication.Responses;

namespace Candidatus.Application.UseCases.Login.DoLogin;

public interface IDoLoginUseCase
{
    Task<ResponseLoginJson> Execute(RequestLoginJson request);
}

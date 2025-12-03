using Candidatus.Communication.Requests;

namespace Candidatus.Application.UseCases.Application.Register;

public interface IRegisterApplicationUseCase
{
    Task Execute(RequestRegisterApplicationJson request);
}

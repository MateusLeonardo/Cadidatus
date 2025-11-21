using Candidatus.Communication.Requests;

namespace Candidatus.Application.UseCases.Platform.Register;

public interface IRegisterPlatformUseCase
{
    Task Execute(RequestRegisterPlatformJson request);
}
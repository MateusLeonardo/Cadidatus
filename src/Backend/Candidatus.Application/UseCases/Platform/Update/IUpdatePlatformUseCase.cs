using Candidatus.Communication.Requests;

namespace Candidatus.Application.UseCases.Platform.Update;

public interface IUpdatePlatformUseCase
{
    Task Execute(int id, RequestUpdatePlatformJson request);
}
using Candidatus.Communication.Responses;

namespace Candidatus.Application.UseCases.Platform.FindAll;

public interface IFindAllPlatformUseCase
{
    Task<ResponseAllPlatformJson> Execute();
}
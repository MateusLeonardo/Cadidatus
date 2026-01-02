using Candidatus.Communication.Responses;

namespace Candidatus.Application.UseCases.Application.FindAll;

public interface IFindAllApplicationUseCase
{
    Task<ResponseAllApplicationsJson> Execute();
}

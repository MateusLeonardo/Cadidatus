using Candidatus.Communication.Responses;

namespace Candidatus.Application.UseCases.State.FindAll;

public interface IFindAllStateUseCase
{
    public Task<ResponseAllStateJson> Execute();
}

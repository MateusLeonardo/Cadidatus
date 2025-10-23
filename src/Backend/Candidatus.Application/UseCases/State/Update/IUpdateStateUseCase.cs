using Candidatus.Communication.Requests;

namespace Candidatus.Application.UseCases.State.Update;

public interface IUpdateStateUseCase
{
    Task Execute(int id, RequestUpdateStateJson request);
}

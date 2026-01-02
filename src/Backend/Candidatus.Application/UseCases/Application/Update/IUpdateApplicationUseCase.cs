using Candidatus.Communication.Requests;

namespace Candidatus.Application.UseCases.Application.Update;

public interface IUpdateApplicationUseCase
{
    Task Execute(int id, RequestUpdateApplicationJson request);
}

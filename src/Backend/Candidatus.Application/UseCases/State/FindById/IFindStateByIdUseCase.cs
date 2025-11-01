using Candidatus.Communication.Responses;

namespace Candidatus.Application.UseCases.State.FindById;

public interface IFindStateByIdUseCase
{
    Task<ResponseStateJson> Execute(int id);
}
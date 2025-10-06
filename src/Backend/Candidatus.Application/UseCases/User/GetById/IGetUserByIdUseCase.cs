using Candidatus.Communication.Responses;

namespace Candidatus.Application.UseCases.User.GetById;

public interface IGetUserByIdUseCase
{
    Task<ResponseUserJson> Execute(int id);
}
using Candidatus.Communication.Responses;

namespace Candidatus.Application.UseCases.User.Profile;

public interface IGetUserProfileUseCase
{
    Task<ResponseUserJson> Execute();
}
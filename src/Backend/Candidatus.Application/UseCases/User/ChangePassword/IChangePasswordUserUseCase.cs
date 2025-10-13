using Candidatus.Communication.Requests;

namespace Candidatus.Application.UseCases.User.ChangePassword;
public interface IChangePasswordUserUseCase
{
    Task Execute(RequestChangePasswordUserJson request);
}

using Candidatus.Communication.Requests;

namespace Candidatus.Application.UseCases.Company.Update;

public interface IUpdateCompanyUseCase
{
    Task Execute(int id, RequestUpdateCompanyJson request);
}
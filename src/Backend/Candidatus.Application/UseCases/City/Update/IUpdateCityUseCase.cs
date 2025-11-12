using Candidatus.Communication.Requests;

namespace Candidatus.Application.UseCases.City.Update;

public interface IUpdateCityUseCase
{
    Task Execute(int id, RequestUpdateCityJson request);
}
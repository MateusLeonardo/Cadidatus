using Candidatus.Communication.Responses;

namespace Candidatus.Application.UseCases.City.FindAll;

public interface IFindAllCitiyUseCase
{
    Task<ResponseAllCityJson> Execute();
}
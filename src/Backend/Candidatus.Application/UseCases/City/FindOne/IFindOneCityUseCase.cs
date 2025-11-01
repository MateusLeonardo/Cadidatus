using Candidatus.Communication.Responses;

namespace Candidatus.Application.UseCases.City.FindOne;

public interface IFindOneCityUseCase
{
    Task<ResponseCityJson> Execute(int id);
}
using Candidatus.Communication.Responses;
using Candidatus.Domain.Repositories.City;
using Candidatus.Domain.Services.LoggedUser;
using Candidatus.Exceptions;
using Candidatus.Exceptions.ExceptionsBase;
using MapsterMapper;

namespace Candidatus.Application.UseCases.City.FindOne;

public class FindOneCityUseCase : IFindOneCityUseCase
{
    private readonly ICityReadOnlyRepository _cityReadOnlyRepository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;
    public FindOneCityUseCase(
        ICityReadOnlyRepository cityReadOnlyRepository, 
        IMapper mapper, 
        ILoggedUser loggedUser)
    {
        _cityReadOnlyRepository = cityReadOnlyRepository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseCityJson> Execute(int id)
    {
        var loggedUser = await _loggedUser.User();
        var city = await _cityReadOnlyRepository.FindById(id, loggedUser);

        if (city is null)
            throw new NotFoundException(ResourceMessagesExceptions.CITY_NOT_FOUND);

        return _mapper.Map<ResponseCityJson>(city);
    }
}
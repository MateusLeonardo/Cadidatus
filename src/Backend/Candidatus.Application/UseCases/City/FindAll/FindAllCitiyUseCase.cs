using Candidatus.Communication.Responses;
using Candidatus.Domain.Repositories.City;
using Candidatus.Domain.Services.LoggedUser;
using MapsterMapper;

namespace Candidatus.Application.UseCases.City.FindAll;

public class FindAllCitiyUseCase : IFindAllCitiyUseCase
{
    private readonly ICityReadOnlyRepository _cityReadOnlyRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;
    public FindAllCitiyUseCase(
        ICityReadOnlyRepository cityReadOnlyRepository,
        ILoggedUser loggedUser,
        IMapper mapper)
    {
        _cityReadOnlyRepository = cityReadOnlyRepository;
        _loggedUser = loggedUser;
        _mapper = mapper;
    }
    public async Task<ResponseAllCityJson> Execute()
    {
        var user = await _loggedUser.User();
        var cities = await _cityReadOnlyRepository.FindAll(user);
        return _mapper.Map<ResponseAllCityJson>(cities);
    }
}
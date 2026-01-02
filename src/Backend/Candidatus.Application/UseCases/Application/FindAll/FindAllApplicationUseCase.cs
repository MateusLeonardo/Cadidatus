using Candidatus.Communication.Responses;
using Candidatus.Domain.Repositories.Application;
using Candidatus.Domain.Services.LoggedUser;
using MapsterMapper;

namespace Candidatus.Application.UseCases.Application.FindAll;

public class FindAllApplicationUseCase : IFindAllApplicationUseCase
{
    private readonly IApplicationReadOnlyRepository _readOnlyRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;
    public FindAllApplicationUseCase(
        IApplicationReadOnlyRepository readOnlyRepository,
        ILoggedUser loggedUser,
        IMapper mapper)
    {
        _readOnlyRepository = readOnlyRepository;
        _loggedUser = loggedUser;
        _mapper = mapper;
    }

    public async Task<ResponseAllApplicationsJson> Execute()
    {
        var loggedUser = await _loggedUser.User();

        var applications = await _readOnlyRepository.FindAll(loggedUser);

        return _mapper.Map<ResponseAllApplicationsJson>(applications);
    }
}

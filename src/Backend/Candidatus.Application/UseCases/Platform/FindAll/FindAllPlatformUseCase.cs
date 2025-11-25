using Candidatus.Communication.Responses;
using Candidatus.Domain.Repositories.Platform;
using Candidatus.Domain.Services.LoggedUser;
using MapsterMapper;

namespace Candidatus.Application.UseCases.Platform.FindAll;

public class FindAllPlatformUseCase : IFindAllPlatformUseCase
{
    private readonly IPlatformReadOnlyRepository _platformReadOnlyRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;
    public FindAllPlatformUseCase(IPlatformReadOnlyRepository platformReadOnlyRepository, ILoggedUser loggedUser, IMapper mapper)
    {
        _platformReadOnlyRepository = platformReadOnlyRepository;
        _loggedUser = loggedUser;
        _mapper = mapper;
    }
    public async Task<ResponseAllPlatformJson> Execute()
    {
        var user = await _loggedUser.User();
        var platforms = await _platformReadOnlyRepository.FindAll(user);
        return _mapper.Map<ResponseAllPlatformJson>(platforms);
    }
}
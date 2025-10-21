using Candidatus.Communication.Responses;
using Candidatus.Domain.Repositories.State;
using Candidatus.Domain.Services.LoggedUser;
using MapsterMapper;

namespace Candidatus.Application.UseCases.State.FindAll;
public class FindAllStateUseCase : IFindAllStateUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IStateReadOnlyRepository _readOnlyRepository;
    private readonly IMapper _mapper;
    public FindAllStateUseCase(
        ILoggedUser loggedUser,
        IStateReadOnlyRepository repository,
        IMapper mapper)
    {
        _loggedUser = loggedUser;
        _readOnlyRepository = repository;
        _mapper = mapper;
    }
    public async Task<ResponseAllStateJson> Execute()
    {
        var loggedUser = await _loggedUser.User();

        var states = await _readOnlyRepository.FindAll(loggedUser);

        return _mapper.Map<ResponseAllStateJson>(states);
    }
}

using Candidatus.Communication.Responses;
using Candidatus.Domain.Repositories.State;
using Candidatus.Domain.Services.LoggedUser;
using Candidatus.Exceptions;
using Candidatus.Exceptions.ExceptionsBase;
using MapsterMapper;

namespace Candidatus.Application.UseCases.State.FindById;

public class FindStateByIdUseCase : IFindStateByIdUseCase
{
    private readonly IStateReadOnlyRepository _readOnlyRepository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;
    public FindStateByIdUseCase(IStateReadOnlyRepository readOnlyRepository, IMapper mapper, ILoggedUser loggedUser)
    {
        _readOnlyRepository = readOnlyRepository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }
    public async Task<ResponseStateJson> Execute(int id)
    {
        var loggedUser = await _loggedUser.User();
        var state = await _readOnlyRepository.FindById(id, loggedUser);

        if (state is null)
            throw new NotFoundException(ResourceMessagesExceptions.STATE_NOT_FOUND);

        return _mapper.Map<ResponseStateJson>(state);
    }
}
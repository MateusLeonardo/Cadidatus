using Candidatus.Communication.Requests;
using Candidatus.Communication.Responses;
using Candidatus.Domain.Repositories;
using Candidatus.Domain.Repositories.State;
using Candidatus.Domain.Services.LoggedUser;
using Candidatus.Exceptions;
using Candidatus.Exceptions.ExceptionsBase;
using MapsterMapper;

namespace Candidatus.Application.UseCases.State.Register;
public class RegisterStateUseCase : IRegisterStateUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IStateWriteOnlyRepository _writeOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStateReadOnlyRepository _readOnlyRepository;
    private readonly IMapper _mapper;

    public RegisterStateUseCase(
        IStateReadOnlyRepository readOnlyRepository,
        IStateWriteOnlyRepository writeOnlyRepository,
        ILoggedUser loggedUser,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _loggedUser = loggedUser;
        _readOnlyRepository = readOnlyRepository;
        _writeOnlyRepository = writeOnlyRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ResponseRegisteredStateJson> Execute(RequestRegisterStateJson request)
    {
        var loggedUser = await _loggedUser.User();
        await Validate(loggedUser, request);

        var state = _mapper.Map<Domain.Entities.State>(request);
        state.UserId = loggedUser.Id;

        await _writeOnlyRepository.Add(state);

        await _unitOfWork.CommitAsync();

        return _mapper.Map<ResponseRegisteredStateJson>(state);
    }

    private async Task Validate(Domain.Entities.User user, RequestRegisterStateJson request)
    {
        var validator = new RegisterStateValidator();
        var result = await validator.ValidateAsync(request);

        var existsUf = await _readOnlyRepository.ExistsWithUf(user, request.Uf);

        if (existsUf)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesExceptions.UF_EXISTS));

        if (!result.IsValid)
            throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());

    }
}

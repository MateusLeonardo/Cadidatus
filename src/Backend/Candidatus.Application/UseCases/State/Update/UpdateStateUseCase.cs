using Candidatus.Communication.Requests;
using Candidatus.Domain.Repositories;
using Candidatus.Domain.Repositories.State;
using Candidatus.Domain.Services.LoggedUser;
using Candidatus.Exceptions;
using Candidatus.Exceptions.ExceptionsBase;

namespace Candidatus.Application.UseCases.State.Update;
public class UpdateStateUseCase : IUpdateStateUseCase
{
    private readonly IStateUpdateOnlyRepository _stateUpdateOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;
    private readonly IStateReadOnlyRepository _stateReadOnlyRepository;
    public UpdateStateUseCase(
        IStateUpdateOnlyRepository stateUpdateOnlyRepository,
        IUnitOfWork unitOfWork,
        ILoggedUser loggedUser,
        IStateReadOnlyRepository stateReadOnlyRepository)
    {
        _stateUpdateOnlyRepository = stateUpdateOnlyRepository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
        _stateReadOnlyRepository = stateReadOnlyRepository;
    }

    public async Task Execute(int id, RequestUpdateStateJson request)
    {
        var loggedUser = await _loggedUser.User();

        var state = await _stateUpdateOnlyRepository.FindById(id, loggedUser);

        if (state is null)
            throw new NotFoundException(ResourceMessagesExceptions.STATE_NOT_FOUND);

        await Validate(request, loggedUser);

        state.Name = request.Name;
        state.Uf = request.Uf;

        _stateUpdateOnlyRepository.Update(state);

        await _unitOfWork.CommitAsync();
    }

    private async Task Validate(RequestUpdateStateJson request, Domain.Entities.User user)
    {
        var validator = new UpdateStateValidator();
        var result = await validator.ValidateAsync(request);

        var newStateUfExists = await _stateReadOnlyRepository.ExistsWithUf(user, request.Uf);

        if (newStateUfExists)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesExceptions.UF_EXISTS));

        if (!result.IsValid)
            throw new ErrorOnValidationException(result.Errors.Select(err => err.ErrorMessage).ToList());
    }
}
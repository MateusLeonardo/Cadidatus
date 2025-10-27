using System;
using Candidatus.Domain.Repositories;
using Candidatus.Domain.Repositories.State;
using Candidatus.Domain.Services.LoggedUser;
using Candidatus.Exceptions;
using Candidatus.Exceptions.ExceptionsBase;

namespace Candidatus.Application.UseCases.State.Delete;

public class DeleteStateUseCase : IDeleteStateUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IStateReadOnlyRepository _readOnlyRepository;
    private readonly IStateWriteOnlyRepository _writeOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteStateUseCase(
        ILoggedUser loggedUser,
        IStateReadOnlyRepository readOnly,
        IStateWriteOnlyRepository writeOnly,
        IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _readOnlyRepository = readOnly;
        _writeOnlyRepository = writeOnly;
        _unitOfWork = unitOfWork;
    }
    public async Task Execute(int id)
    {
        var loggedUser = await _loggedUser.User();

        var state = await _readOnlyRepository.FindById(id, loggedUser);

        if (state is null)
            throw new NotFoundException(ResourceMessagesExceptions.STATE_NOT_FOUND);

        await _writeOnlyRepository.Delete(id);
        
        await _unitOfWork.CommitAsync();
    }
}

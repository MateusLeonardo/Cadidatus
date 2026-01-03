using Candidatus.Domain.Repositories;
using Candidatus.Domain.Repositories.Application;
using Candidatus.Domain.Services.LoggedUser;
using Candidatus.Exceptions;
using Candidatus.Exceptions.ExceptionsBase;

namespace Candidatus.Application.UseCases.Application.Delete;

public class DeleteApplicationUseCase: IDeleteApplicationUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IApplicationWriteOnlyRepository _applicationWriteOnlyRepository;
    private readonly IApplicationReadOnlyRepository _applicationReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteApplicationUseCase(
        ILoggedUser loggedUser, 
        IApplicationReadOnlyRepository readOnlyRepository, 
        IApplicationWriteOnlyRepository writeOnlyRepository,
        IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _applicationReadOnlyRepository = readOnlyRepository;
        _applicationWriteOnlyRepository = writeOnlyRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task Execute(int id)
    {
        var loggedUser = await _loggedUser.User();
        var application = await _applicationReadOnlyRepository.FindById(id, loggedUser);

        if (application is null) throw new NotFoundException(ResourceMessagesExceptions.APPLICATION_NOT_FOUND);

        await _applicationWriteOnlyRepository.Delete(id);

        await _unitOfWork.CommitAsync();
    }
}

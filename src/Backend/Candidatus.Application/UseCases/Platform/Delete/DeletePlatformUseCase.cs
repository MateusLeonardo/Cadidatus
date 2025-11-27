using Candidatus.Domain.Repositories;
using Candidatus.Domain.Repositories.Platform;
using Candidatus.Domain.Services.LoggedUser;
using Candidatus.Exceptions;
using Candidatus.Exceptions.ExceptionsBase;

namespace Candidatus.Application.UseCases.Platform.Delete;

public class DeletePlatformUseCase : IDeletePlatformUseCase
{
    private readonly IPlatformReadOnlyRepository _readOnlyRepository;
    private readonly IPlatformWriteOnlyRepository _writeOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;
    public DeletePlatformUseCase(
        IPlatformReadOnlyRepository readOnlyRepository,
        IPlatformWriteOnlyRepository writeOnlyRepository,
        IUnitOfWork unitOfWork,
        ILoggedUser loggedUser)
    {
        _readOnlyRepository = readOnlyRepository;
        _writeOnlyRepository = writeOnlyRepository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
    }
    public async Task Execute(int id)
    {
        var loggedUser = await _loggedUser.User();

        var platform = await _readOnlyRepository.FindById(id, loggedUser);

        if (platform is null)
            throw new NotFoundException(ResourceMessagesExceptions.PLATFORM_NOT_FOUND);

        await _writeOnlyRepository.Delete(id);

        await _unitOfWork.CommitAsync();
    }
}
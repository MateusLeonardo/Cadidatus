using Candidatus.Domain.Repositories;
using Candidatus.Domain.Repositories.Company;
using Candidatus.Domain.Services.LoggedUser;
using Candidatus.Exceptions;
using Candidatus.Exceptions.ExceptionsBase;

namespace Candidatus.Application.UseCases.Company.Delete;

public class DeleteCompanyUseCase : IDeleteCompanyUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly ICompanyReadOnlyRepository _readOnlyRepository;
    private readonly ICompanyWriteOnlyRepository _writeOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCompanyUseCase(
        ILoggedUser loggedUser,
        ICompanyReadOnlyRepository readOnlyRepository,
        ICompanyWriteOnlyRepository writeOnlyRepository,
        IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _readOnlyRepository = readOnlyRepository;
        _writeOnlyRepository = writeOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(int id)
    {
        var loggedUser = await _loggedUser.User();

        var company = await _readOnlyRepository.FindById(id, loggedUser);
        if (company is null)
            throw new NotFoundException(ResourceMessagesExceptions.COMPANY_NOT_FOUND);

        await _writeOnlyRepository.Delete(id);

        await _unitOfWork.CommitAsync();
    }
}
using Candidatus.Communication.Requests;
using Candidatus.Domain.Extensions;
using Candidatus.Domain.Repositories;
using Candidatus.Domain.Repositories.Application;
using Candidatus.Domain.Repositories.Company;
using Candidatus.Domain.Repositories.Platform;
using Candidatus.Domain.Services.LoggedUser;
using Candidatus.Exceptions;
using Candidatus.Exceptions.ExceptionsBase;
using MapsterMapper;

namespace Candidatus.Application.UseCases.Application.Update;

public class UpdateApplicationUseCase : IUpdateApplicationUseCase
{
    private readonly IApplicationUpdateOnlyRepository _applicationUpdateOnly;
    private readonly ILoggedUser _loggedUser;
    private readonly ICompanyReadOnlyRepository _companyReadOnly;
    private readonly IPlatformReadOnlyRepository _platformReadOnly;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateApplicationUseCase(
        IApplicationUpdateOnlyRepository applicationUpdateOnly, 
        ILoggedUser loggedUser,
        ICompanyReadOnlyRepository companyReadOnly,
        IPlatformReadOnlyRepository platformReadOnly,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _applicationUpdateOnly = applicationUpdateOnly;
        _loggedUser = loggedUser;
        _companyReadOnly = companyReadOnly;
        _platformReadOnly = platformReadOnly;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task Execute(int id, RequestUpdateApplicationJson request)
    {
        var loggedUser = await _loggedUser.User();
        await Validate(request, loggedUser);

        var application = await _applicationUpdateOnly.FindById(id, loggedUser);

        if (application is null)
            throw new NotFoundException(ResourceMessagesExceptions.APPLICATION_NOT_FOUND);

        _mapper.Map(request, application);

        _applicationUpdateOnly.Update(application);

        await _unitOfWork.CommitAsync();
    }

    private async Task Validate(RequestUpdateApplicationJson request, Domain.Entities.User user)
    {
        var companyExists = await _companyReadOnly.Exists(request.CompanyId, user);
        var platformExists = await _platformReadOnly.Exists(request.PlatformId, user);

        var validator = new UpdateApplicationValidator();
        var result = await validator.ValidateAsync(request);

        if(companyExists.IsFalse())
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesExceptions.COMPANY_NOT_FOUND));

        if(platformExists.IsFalse())
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesExceptions.PLATFORM_NOT_FOUND));

        if (result.IsValid.IsFalse())
            throw new ErrorOnValidationException([.. result.Errors.Select(err => err.ErrorMessage)]);
    }
}

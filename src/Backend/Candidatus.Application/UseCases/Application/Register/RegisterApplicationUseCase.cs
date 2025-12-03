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

namespace Candidatus.Application.UseCases.Application.Register;

public class RegisterApplicationUseCase : IRegisterApplicationUseCase
{
    private readonly IApplicationWriteOnlyRepository _applicationWriteOnly;
    private readonly ICompanyReadOnlyRepository _companyReadOnly;
    private readonly IPlatformReadOnlyRepository _platformReadOnly;
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterApplicationUseCase(
        IApplicationWriteOnlyRepository applicationWriteOnly,
        ILoggedUser loggedUser,
        IMapper mapper,
        ICompanyReadOnlyRepository companyReadOnly,
        IPlatformReadOnlyRepository platformReadOnly,
        IUnitOfWork unitOfWork)
    {
        _applicationWriteOnly = applicationWriteOnly;
        _companyReadOnly = companyReadOnly;
        _loggedUser = loggedUser;
        _mapper = mapper;
        _platformReadOnly = platformReadOnly;
        _unitOfWork = unitOfWork;
    }
    public async Task Execute(RequestRegisterApplicationJson request)
    {
        var loggedUser = await _loggedUser.User();
        await Validate(request, loggedUser);

        var application = _mapper.Map<Domain.Entities.Application>(request);
        application.UserId = loggedUser.Id;

        await _applicationWriteOnly.Add(application);

        await _unitOfWork.CommitAsync();
    }

    private async Task Validate(RequestRegisterApplicationJson request, Domain.Entities.User user)
    {
        var validator = new RegisterApplicationValidator();
        var result = await validator.ValidateAsync(request);
        var companyExists = await _companyReadOnly.Exists(request.CompanyId, user);
        var platformExists = await _platformReadOnly.Exists(request.PlatformId, user);

        if (platformExists.IsFalse())
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesExceptions.PLATFORM_NOT_FOUND));

        if (companyExists.IsFalse())
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesExceptions.COMPANY_NOT_FOUND));

        if (!result.IsValid)
            throw new ErrorOnValidationException([.. result.Errors.Select(err => err.ErrorMessage)]);
    }
}

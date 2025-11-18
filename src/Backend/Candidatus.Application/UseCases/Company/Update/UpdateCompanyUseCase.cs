using Candidatus.Communication.Requests;
using Candidatus.Domain.Repositories;
using Candidatus.Domain.Repositories.City;
using Candidatus.Domain.Repositories.Company;
using Candidatus.Domain.Services.LoggedUser;
using Candidatus.Exceptions;
using Candidatus.Exceptions.ExceptionsBase;

namespace Candidatus.Application.UseCases.Company.Update;

public class UpdateCompanyUseCase : IUpdateCompanyUseCase
{
    private readonly ICompanyUpdateOnlyRepository _companyUpdateOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;
    private readonly ICityReadOnlyRepository _cityReadOnlyRepository;
    public UpdateCompanyUseCase(
        ICompanyUpdateOnlyRepository companyUpdateOnlyRepository,
        IUnitOfWork unitOfWork,
        ILoggedUser loggedUser,
        ICityReadOnlyRepository cityReadOnlyRepository)
    {
        _companyUpdateOnlyRepository = companyUpdateOnlyRepository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
        _cityReadOnlyRepository = cityReadOnlyRepository;
    }
    public async Task Execute(int id, RequestUpdateCompanyJson request)
    {
        var loggedUser = await _loggedUser.User();
        var company = await _companyUpdateOnlyRepository.FindById(id, loggedUser);

        if (company is null)
            throw new NotFoundException(ResourceMessagesExceptions.COMPANY_NOT_FOUND);

        await Validate(request, loggedUser);

        company.Name = request.Name;
        company.CityId = request.CityId;

        _companyUpdateOnlyRepository.Update(company);

        await _unitOfWork.CommitAsync();
    }
    private async Task Validate(RequestUpdateCompanyJson request, Domain.Entities.User user)
    {
        var city = await _cityReadOnlyRepository.FindById(request.CityId, user);
        var validator = new UpdateCompanyValidator();
        var result = await validator.ValidateAsync(request);
        
        if (city is null)
            throw new NotFoundException(ResourceMessagesExceptions.CITY_NOT_FOUND);

        if (!result.IsValid)
            throw new ErrorOnValidationException(result.Errors.Select(err => err.ErrorMessage).ToList());
    }
}
using Candidatus.Communication.Requests;
using Candidatus.Communication.Responses;
using Candidatus.Domain.Repositories;
using Candidatus.Domain.Repositories.City;
using Candidatus.Domain.Repositories.Company;
using Candidatus.Domain.Services.LoggedUser;
using Candidatus.Exceptions;
using Candidatus.Exceptions.ExceptionsBase;
using MapsterMapper;

namespace Candidatus.Application.UseCases.Company.Register;

public class RegisterCompanyUseCase : IRegisterCompanyUseCase
{
    private readonly ICompanyWriteOnlyRepository _writeOnlyRepository;
    private readonly ICityReadOnlyRepository _cityReadOnlyRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public RegisterCompanyUseCase(ICompanyWriteOnlyRepository writeOnlyRepository,
        ILoggedUser loggedUser,
        ICityReadOnlyRepository cityReadOnlyRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _cityReadOnlyRepository = cityReadOnlyRepository;
        _loggedUser = loggedUser;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseRegisteredCompanyJson> Execute(RequestRegisterCompanyJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.User();

        var city = await _cityReadOnlyRepository.FindById(request.CityId, loggedUser);    

        if (city is null)
            throw new NotFoundException(ResourceMessagesExceptions.CITY_NOT_FOUND);

        var company = _mapper.Map<Domain.Entities.Company>(request);
        company.UserId = loggedUser.Id;

        await _writeOnlyRepository.Add(company);

        await _unitOfWork.CommitAsync();

        return _mapper.Map<ResponseRegisteredCompanyJson>(company);
    }

    private static void Validate(RequestRegisterCompanyJson request)
    {
        var validator = new RegisterCompanyValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
            throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());
    }
}
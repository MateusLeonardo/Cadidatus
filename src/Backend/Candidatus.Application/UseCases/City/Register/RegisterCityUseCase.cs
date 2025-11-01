using Candidatus.Communication.Requests;
using Candidatus.Communication.Responses;
using Candidatus.Domain.Repositories;
using Candidatus.Domain.Repositories.City;
using Candidatus.Domain.Repositories.State;
using Candidatus.Domain.Services.LoggedUser;
using Candidatus.Exceptions;
using Candidatus.Exceptions.ExceptionsBase;
using MapsterMapper;

namespace Candidatus.Application.UseCases.City.Register;

public class RegisterCityUseCase : IRegisterCityUseCase
{
    private readonly ICityWriteOnlyRepository _cityWriteOnlyRepository;
    private readonly IStateReadOnlyRepository _stateReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;

    public RegisterCityUseCase(
        ICityWriteOnlyRepository cityWriteOnlyRepository,
        IStateReadOnlyRepository stateReadOnlyRepository,
        IUnitOfWork unitOfWork,
        ILoggedUser loggedUser,
        IMapper mapper)
    {
        _cityWriteOnlyRepository = cityWriteOnlyRepository;
        _stateReadOnlyRepository = stateReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
        _mapper = mapper;
    }

    public async Task<ResponseRegisteredCityJson> Execute(RequestRegisterCityJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.User();

        var state = await _stateReadOnlyRepository.FindById(request.StateId, loggedUser);

        if (state is null)
        {
            throw new NotFoundException(ResourceMessagesExceptions.STATE_NOT_FOUND);
        }

        var city = _mapper.Map<Domain.Entities.City>(request);
        city.UserId = loggedUser.Id;

        await _cityWriteOnlyRepository.Add(city);

        await _unitOfWork.CommitAsync();

        return _mapper.Map<ResponseRegisteredCityJson>(city);
    }

    private static void Validate(RequestRegisterCityJson request)
    {
        var validator = new RegisterCityValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
            throw new ErrorOnValidationException([.. result.Errors.Select(err => err.ErrorMessage)]);
    }
}

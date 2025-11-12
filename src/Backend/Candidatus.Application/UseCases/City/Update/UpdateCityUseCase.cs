using Candidatus.Communication.Requests;
using Candidatus.Domain.Repositories;
using Candidatus.Domain.Repositories.City;
using Candidatus.Domain.Repositories.State;
using Candidatus.Domain.Services.LoggedUser;
using Candidatus.Exceptions;
using Candidatus.Exceptions.ExceptionsBase;
using MapsterMapper;

namespace Candidatus.Application.UseCases.City.Update;

public class UpdateCityUseCase : IUpdateCityUseCase
{
    private readonly ICityUpdateOnlyRepository _cityUpdateOnlyRepository;
    private readonly IStateReadOnlyRepository _stateReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;
    public UpdateCityUseCase(
        ICityUpdateOnlyRepository cityUpdateOnlyRepository,
        IStateReadOnlyRepository stateReadOnlyRepository,
        IUnitOfWork unitOfWork,
        ILoggedUser loggedUser)
    {
        _cityUpdateOnlyRepository = cityUpdateOnlyRepository;
        _stateReadOnlyRepository = stateReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
    }
    public async Task Execute(int id, RequestUpdateCityJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.User();

        var city = await _cityUpdateOnlyRepository.FindById(id, loggedUser);
        var state = await _stateReadOnlyRepository.FindById(request.StateId, loggedUser);

        if (city is null)
            throw new NotFoundException(ResourceMessagesExceptions.CITY_NOT_FOUND);
        
        if (state is null)
            throw new NotFoundException(ResourceMessagesExceptions.STATE_NOT_FOUND);

        city.Name = request.Name;
        city.StateId = request.StateId;
        
        _cityUpdateOnlyRepository.Update(city);
        await _unitOfWork.CommitAsync();
    }

    private static void Validate(RequestUpdateCityJson request) {
        var validator = new UpdateCityValidator();
        var result = validator.Validate(request);
        if (!result.IsValid)
            throw new ErrorOnValidationException([.. result.Errors.Select(err => err.ErrorMessage)]);
    }
}
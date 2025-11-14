using System;
using Candidatus.Domain.Repositories;
using Candidatus.Domain.Repositories.City;
using Candidatus.Domain.Services.LoggedUser;
using Candidatus.Exceptions.ExceptionsBase;

namespace Candidatus.Application.UseCases.City.Delete;

public class DeleteCityUseCase : IDeleteCityUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly ICityWriteOnlyRepository _cityWriteOnlyRepository;
    private readonly ICityReadOnlyRepository _cityReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteCityUseCase(
        ILoggedUser loggedUser,
        ICityWriteOnlyRepository cityWriteOnlyRepository,
        ICityReadOnlyRepository cityReadOnlyRepository,
        IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _cityWriteOnlyRepository = cityWriteOnlyRepository;
        _cityReadOnlyRepository = cityReadOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(int id)
    {
        var loggedUser = await _loggedUser.User();

        var city = await _cityReadOnlyRepository.FindById(id, loggedUser);

        if(city is null)
            throw new NotFoundException("City not found.");

        await _cityWriteOnlyRepository.Delete(city.Id);
        await _unitOfWork.CommitAsync();
    }
}

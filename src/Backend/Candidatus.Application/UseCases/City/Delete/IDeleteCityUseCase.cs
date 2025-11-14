using System;

namespace Candidatus.Application.UseCases.City.Delete;

public interface IDeleteCityUseCase
{
    Task Execute(int id);
}

using System;

namespace Candidatus.Application.UseCases.State.Delete;

public interface IDeleteStateUseCase
{
    public Task Execute(int id);
}

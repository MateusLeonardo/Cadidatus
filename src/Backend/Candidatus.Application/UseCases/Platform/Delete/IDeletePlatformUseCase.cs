namespace Candidatus.Application.UseCases.Platform.Delete;

public interface IDeletePlatformUseCase
{
    Task Execute(int id);
}
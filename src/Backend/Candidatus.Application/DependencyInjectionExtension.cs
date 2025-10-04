using Candidatus.Application.Services.Automapper;
using Candidatus.Application.UseCases.User.Register;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Candidatus.Application;
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddUseCases(services);
        AddAutoMapper(services);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Apply(new MapsterConfig());

        services.AddMapster();
    }
}

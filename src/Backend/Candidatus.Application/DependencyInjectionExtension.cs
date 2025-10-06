using Candidatus.Application.Services.Automapper;
using Candidatus.Application.UseCases.User.GetById;
using Candidatus.Application.UseCases.User.Register;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sqids;

namespace Candidatus.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddAutoMapper(services);
        AddIdEncoder(services, configuration);
        AddUseCases(services);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IGetUserByIdUseCase, GetUserByIdUseCase>();
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddScoped(provider =>
        {
            var sqids = provider.GetRequiredService<SqidsEncoder<int>>();

            var config = new TypeAdapterConfig();
            config.Apply(new MapsterConfig(sqids));

            return config;
        });

        services.AddMapster();
    }

    private static void AddIdEncoder(IServiceCollection services, IConfiguration configuration)
    {
        var sqidsEncoder = new SqidsEncoder<int>(new SqidsOptions
        {
            MinLength = 3,
            Alphabet = "k3G7QAe51FCsPW92uEOyq4Bg6Sp8YzVTmnU0liwDdHXLajZrfxNhobJIRcMvKt"
        });

        services.AddSingleton(sqidsEncoder);
    }
}
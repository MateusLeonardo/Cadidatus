using Candidatus.Application.Services.Automapper;
using Candidatus.Application.UseCases.User.Profile;
using Candidatus.Application.UseCases.User.Register;
using Mapster;
using MapsterMapper;
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
        services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddScoped(provider =>
        {
            var sqids = provider.GetRequiredService<SqidsEncoder<int>>();

            var config = new TypeAdapterConfig();

            new MapsterConfig(sqids).Register(config);

            return config;
        });

        services.AddScoped<IMapper, ServiceMapper>();
    }

    private static void AddIdEncoder(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(new SqidsEncoder<int>(new SqidsOptions
        {
            MinLength = 3,
            Alphabet = "k3G7QAe51FCsPW92uEOyq4Bg6Sp8YzVTmnU0liwDdHXLajZrfxNhobJIRcMvKt"
        }));
    }
}
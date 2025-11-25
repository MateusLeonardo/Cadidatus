using Candidatus.Application.Services.Automapper;
using Candidatus.Application.UseCases.City.Delete;
using Candidatus.Application.UseCases.City.FindAll;
using Candidatus.Application.UseCases.City.FindOne;
using Candidatus.Application.UseCases.City.Register;
using Candidatus.Application.UseCases.City.Update;
using Candidatus.Application.UseCases.Company.Delete;
using Candidatus.Application.UseCases.Company.FindAll;
using Candidatus.Application.UseCases.Company.Register;
using Candidatus.Application.UseCases.Company.Update;
using Candidatus.Application.UseCases.Login.DoLogin;
using Candidatus.Application.UseCases.Platform.FindAll;
using Candidatus.Application.UseCases.Platform.Register;
using Candidatus.Application.UseCases.State.Delete;
using Candidatus.Application.UseCases.State.FindAll;
using Candidatus.Application.UseCases.State.FindById;
using Candidatus.Application.UseCases.State.Register;
using Candidatus.Application.UseCases.State.Update;
using Candidatus.Application.UseCases.User.ChangePassword;
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
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();

        services.AddScoped<IChangePasswordUserUseCase, ChangePasswordUserUseCase>();

        services.AddScoped<IRegisterStateUseCase, RegisterStateUseCase>();
        services.AddScoped<IFindAllStateUseCase, FindAllStateUseCase>();
        services.AddScoped<IFindStateByIdUseCase, FindStateByIdUseCase>();
        services.AddScoped<IUpdateStateUseCase, UpdateStateUseCase>();
        services.AddScoped<IDeleteStateUseCase, DeleteStateUseCase>();

        services.AddScoped<IRegisterCityUseCase, RegisterCityUseCase>();
        services.AddScoped<IFindAllCitiyUseCase, FindAllCitiyUseCase>();
        services.AddScoped<IFindOneCityUseCase, FindOneCityUseCase>();
        services.AddScoped<IUpdateCityUseCase, UpdateCityUseCase>();
        services.AddScoped<IDeleteCityUseCase, DeleteCityUseCase>();

        services.AddScoped<IRegisterCompanyUseCase, RegisterCompanyUseCase>();
        services.AddScoped<IFindAllCompanyUseCase, FindAllCompanyUseCase>();
        services.AddScoped<IUpdateCompanyUseCase, UpdateCompanyUseCase>();
        services.AddScoped<IDeleteCompanyUseCase, DeleteCompanyUseCase>();

        services.AddScoped<IRegisterPlatformUseCase, RegisterPlatformUseCase>();
        services.AddScoped<IFindAllPlatformUseCase, FindAllPlatformUseCase>();
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
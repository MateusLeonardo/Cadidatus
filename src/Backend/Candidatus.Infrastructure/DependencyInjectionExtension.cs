using Candidatus.Domain.Repositories;
using Candidatus.Domain.Repositories.Application;
using Candidatus.Domain.Repositories.City;
using Candidatus.Domain.Repositories.Company;
using Candidatus.Domain.Repositories.Platform;
using Candidatus.Domain.Repositories.State;
using Candidatus.Domain.Repositories.User;
using Candidatus.Domain.Security.Cryptography;
using Candidatus.Domain.Security.Tokens;
using Candidatus.Domain.Services.LoggedUser;
using Candidatus.Infrastructure.DataAccess;
using Candidatus.Infrastructure.DataAccess.Repositories;
using Candidatus.Infrastructure.Security.CryptoGraphy;
using Candidatus.Infrastructure.Security.Tokens.Access.Generator;
using Candidatus.Infrastructure.Security.Tokens.Access.Validator;
using Candidatus.Infrastructure.Services.LoggedUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Candidatus.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services);
        AddDbContext(services, configuration);
        AddPasswordEncripter(services);
        AddTokens(services, configuration);
        AddLoggedUser(services);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();

        services.AddScoped<IStateWriteOnlyRepository, StateRepository>();
        services.AddScoped<IStateReadOnlyRepository, StateRepository>();
        services.AddScoped<IStateUpdateOnlyRepository, StateRepository>();

        services.AddScoped<ICityWriteOnlyRepository, CityRepository>();
        services.AddScoped<ICityReadOnlyRepository, CityRepository>();
        services.AddScoped<ICityUpdateOnlyRepository, CityRepository>();

        services.AddScoped<ICompanyWriteOnlyRepository, CompanyRepository>();
        services.AddScoped<ICompanyReadOnlyRepository, CompanyRepository>();
        services.AddScoped<ICompanyUpdateOnlyRepository, CompanyRepository>();

        services.AddScoped<IPlatformWriteOnlyRepository, PlatformRepository>();
        services.AddScoped<IPlatformReadOnlyRepository, PlatformRepository>();
        services.AddScoped<IPlatformUpdateOnlyRepository, PlatformRepository>();

        services.AddScoped<IApplicationWriteOnlyRepository, ApplicationRepository>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<CandidatusDbContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });
    }

    private static void AddPasswordEncripter(IServiceCollection services)
    {
        services.AddScoped<IPasswordEncripter, BCryptNet>();
    }

    private static void AddTokens(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = uint.Parse(configuration.GetSection("Settings:Jwt:ExpirationTimeMinutes").Value!);
        var signinKey = configuration.GetSection("Settings:Jwt:SigningKey").Value!;

        services.AddScoped<IAccessTokenGenerator>(options => new JwtTokenGenerator(expirationTimeMinutes, signinKey));
        services.AddScoped<IAccessTokenValidator>(options => new JwtTokenValidator(signinKey));
    }

    private static void AddLoggedUser(IServiceCollection services)
    {
        services.AddScoped<ILoggedUser, LoggedUser>();
    }
}
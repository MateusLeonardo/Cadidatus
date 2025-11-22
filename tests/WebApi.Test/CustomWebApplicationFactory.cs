using Candidatus.Infrastructure.DataAccess;
using CommonTestUtilities.Entities;
using CommonTestUtilities.IdEncryption;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Test;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private string _password = string.Empty;
    private Candidatus.Domain.Entities.User _user = default!;
    private Candidatus.Domain.Entities.State _state = default!;
    private Candidatus.Domain.Entities.City _city = default!;
    private Candidatus.Domain.Entities.Company _company = default!;
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var descriptor =
                    services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<CandidatusDbContext>));

                if (descriptor is not null)
                    services.Remove(descriptor);

                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<CandidatusDbContext>(option =>
                {
                    option.UseInMemoryDatabase("TestDb");
                    option.UseInternalServiceProvider(provider);
                });

                using var scope = services.BuildServiceProvider().CreateScope();

                var dbContext = scope.ServiceProvider.GetRequiredService<CandidatusDbContext>();
                dbContext.Database.EnsureDeleted();

                StartDatabase(dbContext);
            });
    }

    public string GetEmail()
    {
        return _user.Email;
    }

    public Guid GetUserIdentifier()
    {
        return _user.UserIdentifier;
    }

    public string GetPassword()
    {
        return _password;
    }

    public Candidatus.Domain.Entities.State GetState()
    {
        return _state;
    }

    public Candidatus.Domain.Entities.User GetUser()
    {
        return _user;
    }

    public int GetCompanyId()
    {
        return _company.Id;
    }

    public string GetStateUf() => _state.Uf;

    public int GetStateId() => _state.Id;

    public int GetCityId() => _city.Id;

    private void StartDatabase(CandidatusDbContext dbContext)
    {
        (_user, _password) = UserBuilder.Build();
        _state = StateBuilder.Build(_user);
        _city = CityBuilder.Build(_user, _state);
        _company = CompanyBuilder.Build(_user, _city);

        dbContext.Users.Add(_user);
        dbContext.States.Add(_state);
        dbContext.Cities.Add(_city);
        dbContext.Companies.Add(_company);
        dbContext.SaveChanges();
    }
}
using Candidatus.Communication.Requests;
using Candidatus.Communication.Responses;
using Candidatus.Domain.Entities;
using Mapster;

namespace Candidatus.Application.Services.Automapper;
public class MapsterConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        DomainToResponse(config);
        RequestToDomain(config);
        config.Compile();
    }

    private static void DomainToResponse(TypeAdapterConfig config)
    {
        config.NewConfig<User, ResponseRegisteredUserJson>();
    }

    private static void RequestToDomain(TypeAdapterConfig config)
    {
        config.NewConfig<RequestRegisterUserJson, Domain.Entities.User>()
            .Ignore(u => u.Password);
    }
}

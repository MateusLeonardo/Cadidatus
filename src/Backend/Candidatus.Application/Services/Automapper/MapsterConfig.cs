using Candidatus.Communication.Requests;
using Candidatus.Communication.Responses;
using Candidatus.Domain.Entities;
using Mapster;
using Sqids;

namespace Candidatus.Application.Services.Automapper;

public class MapsterConfig : IRegister
{
    private readonly SqidsEncoder<int> _encoder;

    public MapsterConfig(SqidsEncoder<int> encoder)
    {
        _encoder = encoder;
    }

    public void Register(TypeAdapterConfig config)
    {
        DomainToResponse(config);
        RequestToDomain(config);
        config.Compile();
    }

    private void DomainToResponse(TypeAdapterConfig config)
    {
        config.NewConfig<User, ResponseRegisteredUserJson>()
            .Map(dest => dest.Id, src => _encoder.Encode(src.Id));

        config.NewConfig<User, ResponseUserJson>()
            .Map(dest => dest.Id, src => _encoder.Encode(src.Id));

        config.NewConfig<State, ResponseRegisteredStateJson>();
    }

    private void RequestToDomain(TypeAdapterConfig config)
    {
        config.NewConfig<RequestRegisterUserJson, User>()
            .Ignore(u => u.Password);

        config.NewConfig<RequestRegisterStateJson, State>();
    }
}
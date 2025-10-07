using Candidatus.Application.Services.Automapper;
using CommonTestUtilities.IdEncryption;
using Mapster;
using MapsterMapper;

namespace CommonTestUtilities.Mapper;

public class MapperBuilder
{
    public static IMapper Build()
    {
        var idEncripter = IdEncripterBuilder.Build();
        var config = new TypeAdapterConfig();

        new MapsterConfig(idEncripter).Register(config);

        return new MapsterMapper.Mapper(config);
    }
}
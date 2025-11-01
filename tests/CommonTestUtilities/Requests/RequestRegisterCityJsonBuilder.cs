using Bogus;
using Candidatus.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class RequestRegisterCityJsonBuilder
{
    public static RequestRegisterCityJson Build(int? stateId = null)
    {
        return new Faker<RequestRegisterCityJson>()
            .RuleFor(c => c.Name, f => f.Address.City())
            .RuleFor(c => c.StateId, f => stateId ?? f.Random.Int(1, 100));
    }
}

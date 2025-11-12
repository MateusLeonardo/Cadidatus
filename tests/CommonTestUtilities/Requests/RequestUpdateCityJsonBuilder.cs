using Bogus;
using Candidatus.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class RequestUpdateCityJsonBuilder
{
    public static RequestUpdateCityJson Build(int? stateId = null)
    {
        return new Faker<RequestUpdateCityJson>()
            .RuleFor(r => r.Name, (f) => f.Address.City())
            .RuleFor(r => r.StateId, (f) => stateId ?? f.Random.Int(1, 100));
    }
}
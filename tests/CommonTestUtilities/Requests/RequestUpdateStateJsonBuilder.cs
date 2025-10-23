using Bogus;
using Candidatus.Communication.Requests;

namespace CommonTestUtilities.Requests;
public class RequestUpdateStateJsonBuilder
{
    public static RequestUpdateStateJson Build()
    {
        return new Faker<RequestUpdateStateJson>()
            .RuleFor(r => r.Name, (f) => f.Address.State())
            .RuleFor(r => r.Uf, (f) => f.Address.StateAbbr());
    }

    public static RequestUpdateStateJson BuildWithUfLength(int length)
    {
        return new Faker<RequestUpdateStateJson>()
            .RuleFor(r => r.Name, (f) => f.Address.State())
            .RuleFor(r => r.Uf, (f) => f.Random.String(length));
    }
}

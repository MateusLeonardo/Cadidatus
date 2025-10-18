using Bogus;
using Candidatus.Communication.Requests;

namespace CommonTestUtilities.Requests;
public class RequestRegisterStateJsonBuilder
{
    public static RequestRegisterStateJson Build()
    {
        return new Faker<RequestRegisterStateJson>()
            .RuleFor(s => s.Name, (f) => f.Address.State())
            .RuleFor(s => s.Uf, (f) => f.Address.StateAbbr());
    }

    public static RequestRegisterStateJson BuildWithUfLength(int ufLength)
    {
        return new Faker<RequestRegisterStateJson>()
            .RuleFor(s => s.Name, (f) => f.Address.State())
            .RuleFor(s => s.Uf, (f) => f.Random.String2(ufLength));
    }
}

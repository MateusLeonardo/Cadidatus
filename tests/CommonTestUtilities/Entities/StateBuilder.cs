using Bogus;
using Candidatus.Domain.Entities;

namespace CommonTestUtilities.Entities;
public class StateBuilder
{
    public static State Build()
    {
        return new Faker<State>()
            .RuleFor(s => s.Name, (f) => f.Address.State())
            .RuleFor(s => s.Uf, (f) => f.Address.StateAbbr());
    }
}

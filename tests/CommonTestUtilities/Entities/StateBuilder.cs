using Bogus;
using Candidatus.Domain.Entities;

namespace CommonTestUtilities.Entities;
public class StateBuilder
{
    public static State Build(Candidatus.Domain.Entities.User user)
    {
        return new Faker<State>()
            .RuleFor(s => s.Id, (f) => 1)
            .RuleFor(s => s.Name, (f) => f.Address.State())
            .RuleFor(s => s.Uf, (f) => f.Address.StateAbbr())
            .RuleFor(s => s.UserId, (f) => user.Id);
    }
}

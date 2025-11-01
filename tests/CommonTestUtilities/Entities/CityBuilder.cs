using Bogus;
using Candidatus.Domain.Entities;

namespace CommonTestUtilities.Entities;
public class CityBuilder
{
    public static City Build(User user, State state)
    {
        return new Faker<City>()
            .RuleFor(c => c.Id, (f) => 1)
            .RuleFor(c => c.Name, (f) => f.Address.City())
            .RuleFor(c => c.StateId, (f) => state.Id)
            .RuleFor(c => c.UserId, (f) => user.Id);
    }
}
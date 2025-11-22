using Bogus;
using Candidatus.Domain.Entities;

namespace CommonTestUtilities.Entities;

public class CompanyBuilder
{
    public static Company Build(User user, City city)
    {
        return new Faker<Company>()
            .RuleFor(c => c.Id, (f) => 1)
            .RuleFor(c => c.Name, (f) => f.Company.CompanyName())
            .RuleFor(c => c.UserId, (f) => user.Id)
            .RuleFor(c => c.CityId, (f) => city.Id);
    }
}
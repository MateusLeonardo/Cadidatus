using Bogus;
using Candidatus.Domain.Entities;

namespace CommonTestUtilities.Entities;

public class PlatformBuilder
{
    public static Platform Build(User user)
    {
        return new Faker<Platform>()
            .RuleFor(p => p.Id, (f) => 1)
            .RuleFor(p => p.Name, (f) => f.Company.CompanyName())
            .RuleFor(p => p.Url, (f) => f.Internet.Url())
            .RuleFor(p => p.UserId, (f) => user.Id);
    }
}
using Bogus;
using Candidatus.Domain.Entities;

namespace CommonTestUtilities.Entities;
public class UserBuilder
{
    public static User Build()
    {
        return new Faker<User>()
            .RuleFor(user => user.Id, () => 1)
            .RuleFor(user => user.Email, f => f.Internet.Email())
            .RuleFor(user => user.Password, f => f.Internet.Password())
            .Generate();
    }
}

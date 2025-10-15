using Bogus;
using Candidatus.Communication.Requests;

namespace CommonTestUtilities.Requests;
public class RequestChangePasswordUserJsonBuilder
{
    public static RequestChangePasswordUserJson Build(int lenghtPassword = 10)
    {
        return new Faker<RequestChangePasswordUserJson>()
            .RuleFor(r => r.Password, (f) => f.Internet.Password())
            .RuleFor(r => r.NewPassword, (f) => f.Internet.Password(lenghtPassword));
    }
}

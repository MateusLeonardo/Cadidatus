using Bogus;
using Candidatus.Communication.Requests;

namespace CommonTestUtilities.Requests;
public class RequestChangePasswordBuilder
{
    public static RequestChangePasswordUserJson Build(int passwordLenght = 10)
    {
        return new Faker<RequestChangePasswordUserJson>()
            .RuleFor(r => r.Password, (f) => f.Internet.Password())
            .RuleFor(r => r.NewPassword, (f) => f.Internet.Password(passwordLenght));
    }
}

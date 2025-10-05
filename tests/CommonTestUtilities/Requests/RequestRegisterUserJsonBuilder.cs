using Bogus;
using Candidatus.Communication.Requests;

namespace CommonTestUtilities.Requests;
public class RequestRegisterUserJsonBuilder
{
    public static RequestRegisterUserJson Build(int passwordLength = 10)
    {
        return new Faker<RequestRegisterUserJson>()
            .RuleFor(request => request.Email, (f) => f.Internet.Email())
            .RuleFor(request => request.Password, (f) => f.Internet.Password(passwordLength))
            .Generate();
    }
}

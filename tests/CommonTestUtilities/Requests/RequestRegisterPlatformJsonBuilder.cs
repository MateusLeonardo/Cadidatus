using Bogus;
using Candidatus.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class RequestRegisterPlatformJsonBuilder
{
    public static RequestRegisterPlatformJson Build()
    {
        return new Faker<RequestRegisterPlatformJson>()
            .RuleFor(p => p.Name, (f) => f.Company.CompanyName())
            .RuleFor(p => p.Url, (f) => f.Internet.Url());
    }
}
using Bogus;
using Candidatus.Communication.Requests;

namespace CommonTestUtilities.Requests;
public class RequestUpdatePlatformJsonBuilder
{
    public static RequestUpdatePlatformJson Build()
    {
        return new Faker<RequestUpdatePlatformJson>()
            .RuleFor(r => r.Name, (f) => f.Company.CompanyName())
            .RuleFor(r => r.Url, (f) => f.Internet.Url());
    }
}
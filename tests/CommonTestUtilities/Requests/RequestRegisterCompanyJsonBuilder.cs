using Bogus;
using Candidatus.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class RequestRegisterCompanyJsonBuilder
{
    public static RequestRegisterCompanyJson Build(int? cityId = null)
    {
        return new Faker<RequestRegisterCompanyJson>()
            .RuleFor(r => r.Name, (f) => f.Company.CompanyName())
            .RuleFor(r => r.CityId, (f) => cityId ?? f.Random.Int(1, 100));
    }
}
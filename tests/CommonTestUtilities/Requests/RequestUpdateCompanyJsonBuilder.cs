using Bogus;
using Candidatus.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class RequestUpdateCompanyJsonBuilder
{
    public static RequestUpdateCompanyJson Build(int? cityId = null)
    {
        return new Faker<RequestUpdateCompanyJson>()
            .RuleFor(c => c.Name, f => f.Company.CompanyName())
            .RuleFor(c => c.CityId, f => cityId ?? f.Random.Int(1, 100));
    }
}
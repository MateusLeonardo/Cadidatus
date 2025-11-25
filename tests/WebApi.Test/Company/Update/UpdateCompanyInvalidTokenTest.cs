using System.Net;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace WebApi.Test.Company.Update;

public class UpdateCompanyInvalidTokenTest : CandidatusClassFixture
{
    private readonly string METHOD = "company";
    private readonly int _companyId;
    private readonly int _cityId;

    public UpdateCompanyInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _companyId = factory.GetCompanyId();
        _cityId = factory.GetCityId();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestUpdateCompanyJsonBuilder.Build(_cityId);
        var response = await DoPut($"{METHOD}/{_companyId}", request, "invalidToken");
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
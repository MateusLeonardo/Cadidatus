using System;
using System.Net;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using FluentAssertions;

namespace WebApi.Test.Company.Update;

public class UpdateCompanyTest : CandidatusClassFixture
{
    private readonly string METHOD = "company";
    private readonly Guid _userIdentifier;
    private readonly int _companyId;
    private readonly int _cityId;

    public UpdateCompanyTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
        _companyId = factory.GetCompanyId();
        _cityId = factory.GetCityId();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Buid().Generate(_userIdentifier);
        var request = RequestUpdateCompanyJsonBuilder.Build(_cityId);
        var response = await DoPut($"{METHOD}/{_companyId}", request, token);
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Error_Company_Not_Found()
    {
        var token = JwtTokenGeneratorBuilder.Buid().Generate(_userIdentifier);
        var request = RequestUpdateCompanyJsonBuilder.Build(_cityId);
        var response = await DoPut($"{METHOD}/{99}", request, token);
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}

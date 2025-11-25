using System;
using System.Net;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace WebApi.Test.Company.Register;

public class RegisterCompanyInvalidTokenTest : CandidatusClassFixture
{
    private const string METHOD = "company";
    private readonly int _cityId;
    public RegisterCompanyInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _cityId = factory.GetCityId();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterCompanyJsonBuilder.Build(_cityId);

        var response = await DoPost(METHOD, request, "invalid_token");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}

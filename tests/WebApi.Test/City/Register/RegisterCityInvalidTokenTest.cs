using System.Net;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace WebApi.Test.City.Register;
public class RegisterCityInvalidTokenTest : CandidatusClassFixture
{
    private readonly string METHOD = "city";

    public RegisterCityInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterCityJsonBuilder.Build();
        var response = await DoPost(METHOD, request, "invalidToken");
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
using System.Net;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace WebApi.Test.City.Update;

public class UpdateCityInvalidTokenTest : CandidatusClassFixture
{
    private readonly string METHOD = "city";
    private readonly int _cityId;

    public UpdateCityInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _cityId = factory.GetCityId();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestUpdateCityJsonBuilder.Build();

        var response = await DoPut($"/{METHOD}/{_cityId}", request, "invalidToken");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
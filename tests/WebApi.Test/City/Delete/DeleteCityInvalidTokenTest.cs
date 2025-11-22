using System.Net;
using FluentAssertions;

namespace WebApi.Test.City.Delete;

public class DeleteCityInvalidTokenTest : CandidatusClassFixture
{
    private const string METHOD = "city";
    private readonly int _cityId;
    public DeleteCityInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _cityId = factory.GetCityId();
    }

    [Fact]
    public async Task Success()
    {
        var response = await DoDelete($"{METHOD}/{_cityId}", "invalidToken");
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
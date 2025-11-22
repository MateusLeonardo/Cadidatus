using System.Net;
using CommonTestUtilities.Tokens;
using FluentAssertions;

namespace WebApi.Test.City.Delete;

public class DeleteCityTest : CandidatusClassFixture
{
    private const string METHOD = "city";
    private readonly int _cityId;
    private readonly Guid _userIdentifier;
    public DeleteCityTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _cityId = factory.GetCityId();
        _userIdentifier = factory.GetUserIdentifier();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Buid().Generate(_userIdentifier);
        var response = await DoDelete($"{METHOD}/{_cityId}", token);
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task City_Not_Found()
    {
        var token = JwtTokenGeneratorBuilder.Buid().Generate(_userIdentifier);
        var response = await DoDelete($"{METHOD}/999999", token);
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
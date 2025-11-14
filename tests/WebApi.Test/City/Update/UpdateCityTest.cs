using System.Net;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using FluentAssertions;

namespace WebApi.Test.City.Update;

public class UpdateCityTest : CandidatusClassFixture
{
    private const string METHOD = "city";
    private readonly Guid _userIdentifier;
    private readonly int _stateId;
    private readonly int _cityId;

    public UpdateCityTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
        _stateId = factory.GetStateId();
        _cityId = factory.GetCityId();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestUpdateCityJsonBuilder.Build(_stateId);
        var token = JwtTokenGeneratorBuilder.Buid().Generate(_userIdentifier);
        var response = await DoPut($"{METHOD}/{_cityId}", request, token);
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
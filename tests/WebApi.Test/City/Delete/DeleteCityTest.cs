using System.Net;
using System.Text.Json;
using Candidatus.Exceptions;
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

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        errors.Should().ContainSingle().And.Contain(err => err.GetString()!.Contains(ResourceMessagesExceptions.CITY_NOT_FOUND));
    }
}
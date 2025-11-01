using System.Net;
using System.Text.Json;
using Candidatus.Exceptions;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using FluentAssertions;

namespace WebApi.Test.City.Register;

public class RegisterCityTest : CandidatusClassFixture
{
    private const string METHOD = "city";
    private readonly Guid _userIdentifier;
    private readonly int _stateId;
    public RegisterCityTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
        _stateId = factory.GetStateId();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterCityJsonBuilder.Build(_stateId);
        var token = JwtTokenGeneratorBuilder.Buid().Generate(_userIdentifier);

        var response = await DoPost(METHOD, request, token);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("name").GetString().Should().NotBeNullOrWhiteSpace()
            .And.Be(request.Name);
    }

    [Fact]
    public async Task Error_City_Name_Empty()
    {
        var token = JwtTokenGeneratorBuilder.Buid().Generate(_userIdentifier);
        var request = RequestRegisterCityJsonBuilder.Build(_stateId);
        request.Name = string.Empty;

        var response = await DoPost(METHOD, request, token);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        errors.Should().ContainSingle().And.Contain(e => e.GetString()!.Equals(ResourceMessagesExceptions.CITY_EMPTY));
    }

    [Fact]
    public async Task Error_State_Not_Found()
    {
        var token = JwtTokenGeneratorBuilder.Buid().Generate(_userIdentifier);
        var request = RequestRegisterCityJsonBuilder.Build(10);

        var response = await DoPost(METHOD, request, token);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();
        errors.Should().ContainSingle().And.Contain(e => e.GetString()!.Equals(ResourceMessagesExceptions.STATE_NOT_FOUND));
    }
}
using System.Net;
using System.Text.Json;
using Candidatus.Exceptions;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using FluentAssertions;

namespace WebApi.Test.Platform.Update;

public class UpdatePlatformTest : CandidatusClassFixture
{
    private readonly string METHOD = "platform";
    private readonly Guid _userIdentifier;
    private readonly int _platformId;
    public UpdatePlatformTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
        _platformId = factory.GetPlatformId();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Buid().Generate(_userIdentifier);
        var request = RequestUpdatePlatformJsonBuilder.Build();

        var response = await DoPut($"{METHOD}/{_platformId}", request, token);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Error_Platform_Not_Found()
    {
        var token = JwtTokenGeneratorBuilder.Buid().Generate(_userIdentifier);
        var request = RequestUpdatePlatformJsonBuilder.Build();

        var response = await DoPut($"{METHOD}/1000", request, token);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        errors.Should().ContainSingle().And.Contain(e => e.GetString()!.Equals(ResourceMessagesExceptions.PLATFORM_NOT_FOUND));
    }

    [Fact]
    public async Task Error_Platform_Name_Empty()
    {
        var token = JwtTokenGeneratorBuilder.Buid().Generate(_userIdentifier);
        var request = RequestUpdatePlatformJsonBuilder.Build();
        request.Name = string.Empty;

        var response = await DoPut($"{METHOD}/{_platformId}", request, token);
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        errors.Should().ContainSingle().And.Contain(e => e.GetString()!.Equals(ResourceMessagesExceptions.PLATFORM_NAME_EMPTY));
    }

    [Fact]
    public async Task Error_Platform_Url_Invalid()
    {
        var token = JwtTokenGeneratorBuilder.Buid().Generate(_userIdentifier);
        var request = RequestUpdatePlatformJsonBuilder.Build();
        request.Url = "invalid-url";

        var response = await DoPut($"{METHOD}/{_platformId}", request, token);
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        errors.Should().ContainSingle().And.Contain(e => e.GetString()!.Equals(ResourceMessagesExceptions.URL_INVALID));
    }
}
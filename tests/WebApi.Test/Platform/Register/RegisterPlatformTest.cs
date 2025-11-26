using System.Net;
using System.Text.Json;
using Candidatus.Exceptions;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using FluentAssertions;

namespace WebApi.Test.Platform.Register;

public class RegisterPlatformTest : CandidatusClassFixture
{
    private const string METHOD = "platform";
    private readonly Guid _userIdentifier;
    public RegisterPlatformTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Buid().Generate(_userIdentifier);
        var request = RequestRegisterPlatformJsonBuilder.Build();

        var response = await DoPost(METHOD, request, token);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Error_Platform_Name_Empty()
    {
        var token = JwtTokenGeneratorBuilder.Buid().Generate(_userIdentifier);
        var request = RequestRegisterPlatformJsonBuilder.Build();
        request.Name = string.Empty;

        var response = await DoPost(METHOD, request, token);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        errors.Should().ContainSingle().And.Contain(e => e.GetString()!.Contains(ResourceMessagesExceptions.PLATFORM_NAME_EMPTY));
    }

    [Fact]
    public async Task Error_Platform_Url_Invalid()
    {
        var token = JwtTokenGeneratorBuilder.Buid().Generate(_userIdentifier);
        var request = RequestRegisterPlatformJsonBuilder.Build();
        request.Url = "invalid-url";

        var response = await DoPost(METHOD, request, token);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        errors.Should().ContainSingle().And.Contain(e => e.GetString()!.Contains(ResourceMessagesExceptions.URL_INVALID));
    }
}

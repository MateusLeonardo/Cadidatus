using Candidatus.Exceptions;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.State.Register;
public class RegisterStateTest : CandidatusClassFixture
{
    private readonly string METHOD = "state";
    private readonly Guid _userIdentifier;
    private readonly string _uf;

    public RegisterStateTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
        _uf = factory.GetStateUf();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterStateJsonBuilder.Build();
        var token = JwtTokenGeneratorBuilder.Buid().Generate(_userIdentifier);

        var response = await DoPost(METHOD, request, token);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("name").GetString().Should().NotBeNullOrWhiteSpace()
            .And.Be(request.Name);
    }

    [Fact]
    public async Task Error_Uf_Exists()
    {
        var request = RequestRegisterStateJsonBuilder.Build();
        request.Uf = _uf;
        var token = JwtTokenGeneratorBuilder.Buid().Generate(_userIdentifier);

        var response = await DoPost(METHOD, request, token);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        errors.Should().ContainSingle().And.Contain(e => e.GetString()!.Equals(ResourceMessagesExceptions.UF_EXISTS));
    }
}

using System;
using System.Net;
using System.Text.Json;
using Candidatus.Exceptions;
using CommonTestUtilities.Tokens;
using FluentAssertions;

namespace WebApi.Test.State.Delete;

public class DeleteStateTest : CandidatusClassFixture
{
    private const string METHOD = "state";
    private readonly string _stateId;
    private readonly Guid _userIdentifier;
    public DeleteStateTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _stateId = factory.GetStateId();
        _userIdentifier = factory.GetUserIdentifier();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Buid().Generate(_userIdentifier);

        var response = await DoDelete($"{METHOD}/{_stateId}", token);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Error_State_Not_Found()
    {
        var token = JwtTokenGeneratorBuilder.Buid().Generate(_userIdentifier);

        var response = await DoDelete($"{METHOD}/22", token);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        errors.Should().ContainSingle().And
            .Contain(err => err.GetString()!.Contains(ResourceMessagesExceptions.STATE_NOT_FOUND));
    }
}

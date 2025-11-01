using Candidatus.Exceptions;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.State.Update;

public class UpdateStateTest : CandidatusClassFixture
{
    private readonly string METHOD = "state";
    private readonly Candidatus.Domain.Entities.State _state;
    private readonly Candidatus.Domain.Entities.User _user;
    private readonly int _stateId;
    public UpdateStateTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _state = factory.GetState();
        _user = factory.GetUser();
        _stateId = factory.GetStateId();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Buid().Generate(_user.UserIdentifier);
        var request = RequestUpdateStateJsonBuilder.Build();

        var response = await DoPut($"{METHOD}/{_stateId}", request, token);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Error_State_Not_Found()
    {
        var token = JwtTokenGeneratorBuilder.Buid().Generate(_user.UserIdentifier);
        var request = RequestUpdateStateJsonBuilder.Build();
        var response = await DoPut($"{METHOD}/1000", request, token);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        errors.Should().ContainSingle().And
            .Contain(err => err.GetString()!.Equals(ResourceMessagesExceptions.STATE_NOT_FOUND));
    }
}

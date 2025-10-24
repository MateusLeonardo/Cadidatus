using System;
using System.Net;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using FluentAssertions;

namespace WebApi.Test.State.Update;

public class UpdateStateInvalidTokenTest : CandidatusClassFixture
{
    private readonly string METHOD = "state";
    private readonly Candidatus.Domain.Entities.State _state;
    public UpdateStateInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _state = factory.GetState();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestUpdateStateJsonBuilder.Build();
        var token = JwtTokenGeneratorBuilder.Buid().Generate(Guid.NewGuid());

        var response = await DoPut($"{METHOD}/{_state.Id}", request, token);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}

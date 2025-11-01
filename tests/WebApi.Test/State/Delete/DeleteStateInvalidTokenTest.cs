using System.Net;
using CommonTestUtilities.Tokens;
using FluentAssertions;

namespace WebApi.Test.State.Delete;

public class DeleteStateInvalidTokenTest : CandidatusClassFixture
{
    private const string METHOD = "state";
    private readonly int _stateId;
    public DeleteStateInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _stateId = factory.GetStateId();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Buid().Generate(Guid.NewGuid());

        var response = await DoDelete($"{METHOD}/{_stateId}", token);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}

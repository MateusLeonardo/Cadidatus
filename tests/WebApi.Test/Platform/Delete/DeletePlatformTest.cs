using System.Net;
using CommonTestUtilities.Tokens;
using FluentAssertions;

namespace WebApi.Test.Platform.Delete;

public class DeletePlatformTest : CandidatusClassFixture
{
    private readonly string METHOD = "platform";
    private readonly int _platformId;
    private readonly Guid _userIdentifier;
    public DeletePlatformTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _platformId = factory.GetPlatformId();
        _userIdentifier = factory.GetUserIdentifier();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Buid().Generate(_userIdentifier);

        var response = await DoDelete($"{METHOD}/{_platformId}", token);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
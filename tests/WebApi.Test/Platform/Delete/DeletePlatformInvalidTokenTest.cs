using System.Net;
using FluentAssertions;

namespace WebApi.Test.Platform.Delete;

public class DeletePlatformInvalidTokenTest : CandidatusClassFixture
{
    private readonly string METHOD = "platform";
    private readonly int _platformId;
    public DeletePlatformInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _platformId = factory.GetPlatformId();
    }

    [Fact]
    public async Task Success()
    {
        var response = await DoDelete($"{METHOD}/{_platformId}", "invalidToken");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
using CommonTestUtilities.Requests;
using FluentAssertions;
using System.Net;

namespace WebApi.Test.State.Register;
public class RegisterStateInvalidTokenTest : CandidatusClassFixture
{
    private readonly string METHOD = "state";
    public RegisterStateInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterStateJsonBuilder.Build();

        var response = await DoPost(METHOD, request, "invalidToken");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}

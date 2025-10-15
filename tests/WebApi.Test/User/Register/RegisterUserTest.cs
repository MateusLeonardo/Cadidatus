using Candidatus.Exceptions;
using CommonTestUtilities.Requests;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.User.Register;

public class RegisterUserTest : CandidatusClassFixture
{
    private readonly string METHOD = "user";

    public RegisterUserTest(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        var response = await DoPost(METHOD, request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("email").GetString().Should().NotBeNullOrWhiteSpace().And
            .Be(request.Email);
        responseData.RootElement.GetProperty("tokens").GetProperty("accessToken").GetString().Should()
            .NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Error_Email_Empty()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = string.Empty;

        var response = await DoPost(METHOD, request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        errors.Should().ContainSingle().And.Contain(e => e.GetString()!.Equals(ResourceMessagesExceptions.EMAIL_EMPTY));
    }

    [Fact]
    public async Task Error_Password_Empty()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Password = string.Empty;

        var response = await DoPost(METHOD, request);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        using var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        errors.Should().ContainSingle().And
            .Contain(e => e.GetString()!.Equals(ResourceMessagesExceptions.PASSWORD_EMPTY));
    }
}
using System.Net;
using System.Text.Json;
using Candidatus.Exceptions;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using FluentAssertions;

namespace WebApi.Test.Company.Register;

public class RegisterCompanyTest : CandidatusClassFixture
{
    private const string METHOD = "company";
    private readonly Guid _userIdentifier;
    private readonly int _cityId;
    public RegisterCompanyTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
        _cityId = factory.GetCityId();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Buid().Generate(_userIdentifier);
        var request = RequestRegisterCompanyJsonBuilder.Build(_cityId);

        var response = await DoPost(METHOD, request, token);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Error_Company_Name_Empty()
    {
        var token = JwtTokenGeneratorBuilder.Buid().Generate(_userIdentifier);
        var request = RequestRegisterCompanyJsonBuilder.Build(_cityId);
        request.Name = string.Empty;

        var response = await DoPost(METHOD, request, token);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();
        errors.Should().ContainSingle().And.Contain(e => e.GetString()!.Contains(ResourceMessagesExceptions.COMPANY_EMPTY));
    }

    [Fact]
    public async Task Error_City_Not_Found()
    {
        var token = JwtTokenGeneratorBuilder.Buid().Generate(_userIdentifier);
        var request = RequestRegisterCompanyJsonBuilder.Build(99);
        var response = await DoPost(METHOD, request, token);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();
        errors.Should().ContainSingle().And.Contain(e => e.GetString()!.Contains(ResourceMessagesExceptions.CITY_NOT_FOUND));
    }
}

using System.Net;
using CommonTestUtilities.Tokens;
using FluentAssertions;

namespace WebApi.Test.Company.Delete;

public class DeleteCompanyTest : CandidatusClassFixture
{
    private const string METHOD = "company";
    private readonly int _companyId;
    private readonly Guid _userIdentifier;
    public DeleteCompanyTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _companyId = factory.GetCompanyId();
        _userIdentifier = factory.GetUserIdentifier();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Buid().Generate(_userIdentifier);
        var response = await DoDelete($"{METHOD}/{_companyId}", token);
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Error_Company_Not_Found()
    {
        var token = JwtTokenGeneratorBuilder.Buid().Generate(_userIdentifier);
        var response = await DoDelete($"{METHOD}/999999", token);
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
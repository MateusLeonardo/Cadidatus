using System.Net;
using FluentAssertions;

namespace WebApi.Test.Company.Delete;

public class DeleteCompanyInvalidTokenTest : CandidatusClassFixture
{
    private const string METHOD = "company";
    private readonly int _companyId;
    public DeleteCompanyInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _companyId = factory.GetCompanyId();
    }

    [Fact]
    public async Task Success()
    {
        var response = await DoDelete($"{METHOD}/{_companyId}", "invalidToken");
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
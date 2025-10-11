using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace WebApi.Test;

public class CandidatusClassFixture : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;

    protected CandidatusClassFixture(CustomWebApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();
    }

    protected async Task<HttpResponseMessage> DoPost(string method, object request, string token = "")
    {
        AuthorizeRequest(token);

        return await _httpClient.PostAsJsonAsync(method, request);
    }

    protected async Task<HttpResponseMessage> DoGet(string method, string token = "")
    {
        AuthorizeRequest(token);
        return await _httpClient.GetAsync(method);
    }

    private void AuthorizeRequest(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            return;

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using Navtor.ERP.API.Models;
using Newtonsoft.Json;

namespace Navtor.ERP.API;

internal class NavtorApiService
{
    private const string BaseUrl = "https://navtor-erpapi.azurewebsites.net/api/v2/";
    private const string TokenEndpoint = "https://api.navtor.com/token";
    private const string ClientId = "navtor_navserver";
    private const string ClientSecret = "L4BnNYaJGOcCdpY8cCiZ0QiP8CCiRjdhbF9hV2sxD6MK7goT8yD62Cw1iHHXiXZm5eVhrhrvV0F3BhSPq5DNJJOE8wLMwR5sQK71otmWcAey5hgrdTmff3cmcGNtsvEz";
    private const string Username = "navtor.voyager_vessel_track_reader";
    private const string Password = "h6vP0Jzit8F2nTYibZkNCoBI2j5Mo0gy";

    protected static async Task<string> HttpGetAsync(string apiMethod)
    {
        string token = await GetTokenAsync(TokenEndpoint, ClientId, ClientSecret, Username, Password);

        using var client = new HttpClient();

        // Set the Authorization header with the Bearer token
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Send the GET request
        HttpResponseMessage response = await client.GetAsync($"{BaseUrl}{apiMethod}");

        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            // Log the error
            Console.WriteLine($"Error: {response.StatusCode}");
            throw new Exception(responseContent);
        }

        // Read and return the response content
        return await response.Content.ReadAsStringAsync();
    }

    protected static List<T> JsonToObjects<T>(string json) where T : class
        => JsonConvert.DeserializeObject<List<T>>(json) ?? [];

    private static async Task<string> GetTokenAsync(string tokenEndpoint, string clientId, string clientSecret, string username, string password)
    {
        using var client = new HttpClient();
        // Create the request
        var request = new HttpRequestMessage(HttpMethod.Post, tokenEndpoint);
        var content = new FormUrlEncodedContent(
        [
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("client_id", clientId),
            new KeyValuePair<string, string>("client_secret", clientSecret),
            new KeyValuePair<string, string>("username", username),
            new KeyValuePair<string, string>("password", password)
        ]);
        request.Content = content;

        // Send the request
        HttpResponseMessage response = await client.SendAsync(request);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            // Log the error
            Console.WriteLine($"Error: {response.StatusCode}");
            throw new Exception(responseContent);
        }

        JObject tokenObject = JObject.Parse(responseContent);
        return tokenObject["access_token"]?.ToString() ?? string.Empty;
    }
}
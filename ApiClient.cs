using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatsJingleBang;
public class ApiClient
{
    private readonly HttpClient _httpClient;
    private const string BASE_URL = "https://games.datsteam.dev/";
    private const string TOKEN = GameConstants.Token;

    public ApiClient()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(BASE_URL);
        _httpClient.DefaultRequestHeaders.Add("X-Auth-Token", TOKEN);
    }


    public async Task<string> GetMapAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/arena");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching map: {ex.Message}");
            throw;
        }
    }
}

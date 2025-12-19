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

    public async Task<string> GetBoostersAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/booster");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching boosters: {ex.Message}");
            return "";
        }
    }

    public async Task<string> GetCheatCode()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/cheatcode");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("ex " + ex);
            return null;
        }
      
    }
    public async Task PostActivateBooster()
    {
        try
        {
            var response = await _httpClient.PostAsync("api/booster/", null);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error activating booster: {ex.Message}");
        }
    }

}

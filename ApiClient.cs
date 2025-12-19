using System.Net.Http.Json;
using System.Text.Json;

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

    // JSON опции для сериализации (можно вынести в отдельный класс)
    private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    // 1. /api/arena - получение состояния арены
    public async Task<PlayerResponse> GetArenaAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/arena");
            
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PlayerResponse>(JsonOptions) 
                       ?? throw new InvalidOperationException("Response content is null");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var error = await response.Content.ReadFromJsonAsync<PublicError>(JsonOptions);
                throw new ApiException($"Bad Request: {string.Join(", ", error?.Errors ?? new List<string>())}", response.StatusCode);
            }
            
            response.EnsureSuccessStatusCode();
            throw new InvalidOperationException("Unexpected response");
        }
        catch (HttpRequestException ex)
        {
            throw new ApiException($"Error fetching arena: {ex.Message}", ex.StatusCode ?? System.Net.HttpStatusCode.InternalServerError);
        }
    }

    // 2. /api/booster (GET) - получение доступных бустеров
    public async Task<AvailableBoosterResponse> GetAvailableBoostersAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/booster");
            
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<AvailableBoosterResponse>(JsonOptions) 
                       ?? throw new InvalidOperationException("Response content is null");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                var error = await response.Content.ReadFromJsonAsync<PublicError>(JsonOptions);
                throw new ApiException($"Forbidden: {string.Join(", ", error?.Errors ?? new List<string>())}", response.StatusCode);
            }
            
            response.EnsureSuccessStatusCode();
            throw new InvalidOperationException("Unexpected response");
        }
        catch (HttpRequestException ex)
        {
            throw new ApiException($"Error fetching boosters: {ex.Message}", ex.StatusCode ?? System.Net.HttpStatusCode.InternalServerError);
        }
    }

    // 2. /api/booster (POST) - активация бустера
    public async Task<PublicError> ActivateBoosterAsync(CommandBooster boosterCommand)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/booster", boosterCommand, JsonOptions);
            
            if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return await response.Content.ReadFromJsonAsync<PublicError>(JsonOptions) 
                       ?? throw new InvalidOperationException("Response content is null");
            }
            
            response.EnsureSuccessStatusCode();
            throw new InvalidOperationException("Unexpected response");
        }
        catch (HttpRequestException ex)
        {
            throw new ApiException($"Error activating booster: {ex.Message}", ex.StatusCode ?? System.Net.HttpStatusCode.InternalServerError);
        }
    }

    // 3. /api/cheatcode (POST) - применение читов
    public async Task<CheatCodeResponse> ApplyCheatCodeAsync(CheatCodeCommand cheatCommand)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/cheatcode", cheatCommand, JsonOptions);
            
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CheatCodeResponse>(JsonOptions) 
                       ?? throw new InvalidOperationException("Response content is null");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var error = await response.Content.ReadFromJsonAsync<PublicError>(JsonOptions);
                throw new ApiException($"Bad Request: {string.Join(", ", error?.Errors ?? new List<string>())}", response.StatusCode);
            }
            
            response.EnsureSuccessStatusCode();
            throw new InvalidOperationException("Unexpected response");
        }
        catch (HttpRequestException ex)
        {
            throw new ApiException($"Error applying cheat code: {ex.Message}", ex.StatusCode ?? System.Net.HttpStatusCode.InternalServerError);
        }
    }

    // 4. /api/logs (GET) - получение логов игрока
    public async Task<List<LogMessage>> GetPlayerLogsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/logs");
            
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<LogMessage>>(JsonOptions) 
                       ?? new List<LogMessage>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var error = await response.Content.ReadFromJsonAsync<PublicError>(JsonOptions);
                throw new ApiException($"Bad Request: {string.Join(", ", error?.Errors ?? new List<string>())}", response.StatusCode);
            }
            
            response.EnsureSuccessStatusCode();
            throw new InvalidOperationException("Unexpected response");
        }
        catch (HttpRequestException ex)
        {
            throw new ApiException($"Error fetching logs: {ex.Message}", ex.StatusCode ?? System.Net.HttpStatusCode.InternalServerError);
        }
    }

    // 5. /api/move (POST) - отправка команд движения
    public async Task<PublicError> SendMoveCommandAsync(PlayerCommand moveCommand)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/move", moveCommand, JsonOptions);
            
            if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return await response.Content.ReadFromJsonAsync<PublicError>(JsonOptions) 
                       ?? throw new InvalidOperationException("Response content is null");
            }
            
            response.EnsureSuccessStatusCode();
            throw new InvalidOperationException("Unexpected response");
        }
        catch (HttpRequestException ex)
        {
            throw new ApiException($"Error sending move command: {ex.Message}", ex.StatusCode ?? System.Net.HttpStatusCode.InternalServerError);
        }
    }

    // 6. /api/rounds (GET) - получение информации о раундах
    public async Task<RoundListResponse> GetRoundsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/rounds");
            
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<RoundListResponse>(JsonOptions) 
                       ?? throw new InvalidOperationException("Response content is null");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest || 
                     response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                var error = await response.Content.ReadFromJsonAsync<PublicError>(JsonOptions);
                throw new ApiException($"Error: {string.Join(", ", error?.Errors ?? new List<string>())}", response.StatusCode);
            }
            
            response.EnsureSuccessStatusCode();
            throw new InvalidOperationException("Unexpected response");
        }
        catch (HttpRequestException ex)
        {
            throw new ApiException($"Error fetching rounds: {ex.Message}", ex.StatusCode ?? System.Net.HttpStatusCode.InternalServerError);
        }
    }

    
}

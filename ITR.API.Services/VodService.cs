using ITR.API.Services;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

public class VodService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiBaseUrl;
    private readonly string _apiKey;
    private readonly string _apiSecret;
    private readonly string _lmsId;

    public VodService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _apiBaseUrl = config["Vod:ApiBaseUrl"];
        _apiKey = config["Vod:ApiKey"];
        _apiSecret = config["Vod:ApiSecret"];
        _lmsId = config["Vod:LmsId"];
    }

    private string ComputeSignature(string apiKey, string apiSecret, string timestamp, string payload)
    {
        var stringToSign = apiKey + timestamp + payload;
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(apiSecret));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(stringToSign));
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }

    public async Task<string> FetchFoldersAsync()
    {
        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
        var payload = ""; // GET request => no body
        var signature = ComputeSignature(_apiKey, _apiSecret, timestamp, payload);

        var request = new HttpRequestMessage(
            HttpMethod.Get,
            $"{_apiBaseUrl}/lms/get-folders/{_lmsId}"
        );

        request.Headers.Add("X-API-Key", _apiKey);
        request.Headers.Add("X-Timestamp", timestamp);
        request.Headers.Add("X-Signature", signature);
        request.Headers.Add("Accept", "application/json");

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync(); // return raw JSON
    }

    public async Task<string> ProcessLectureAsync(string uuid, string folderId, List<string> qualities)
    {
        var formData = new Dictionary<string, string>
    {
        { "lms_id", _lmsId },
        { "uuid", uuid },
        { "folder_id", folderId },
        { "qualities", JsonSerializer.Serialize(qualities) } 
    };


        var payloadForm = $"lms_id={_lmsId}&uuid={uuid}&folder_id={folderId}&qualities={JsonSerializer.Serialize(qualities)}";

        var request = new HttpRequestMessage(HttpMethod.Post, $"{_apiBaseUrl}/lms/process-lecture")
        {
            Content = new FormUrlEncodedContent(formData)
        };


        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
        var stringToSign = _apiKey + timestamp + payloadForm;

        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_apiSecret));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(stringToSign));
        var signature = BitConverter.ToString(hash).Replace("-", "").ToLower();

        request.Headers.Add("X-API-Key", _apiKey);
        request.Headers.Add("X-Timestamp", timestamp);
        request.Headers.Add("X-Signature", signature);

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<LectureLinksResponse> SignLectureAsync(string uuid)
    {
        var signingUrl = $"{_apiBaseUrl}/signing-hls";

        var formData = new Dictionary<string, string>
    {
        { "lms_id", _lmsId },
        { "uuid", uuid }
    };

        // ✨ لازم نعمل string payload بنفس الشكل عشان التوقيع
        var payloadForm = $"lms_id={_lmsId}&uuid={uuid}";

        var request = new HttpRequestMessage(HttpMethod.Post, signingUrl)
        {
            Content = new FormUrlEncodedContent(formData)
        };

        // 🟢 التوقيع
        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
        var stringToSign = _apiKey + timestamp + payloadForm;

        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_apiSecret));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(stringToSign));
        var signature = BitConverter.ToString(hash).Replace("-", "").ToLower();

        request.Headers.Add("X-API-Key", _apiKey);
        request.Headers.Add("X-Timestamp", timestamp);
        request.Headers.Add("X-Signature", signature);

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<LectureLinksResponse>(body, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        string Ahme = "";

        return result!;
    }





}

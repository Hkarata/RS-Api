using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RSAllies.SMS.Contracts.Requests;

namespace RSAllies.SMS.Services;

public class MessageService(ILogger<MessageService> logger)
{
    private static readonly string apiKey = "23ff38b59d83ee03";
    private static readonly string secretKey = "OWZkMDcyN2Y1NmFlNzU1OTFkNDhjZjJhNzhiMzE0OTZhMDljNGNkZDZkZGE2NmI1NjYwMjE0NTZmOGVmZWNiNg==";
    private static readonly string contentType = "application/json";
    private static readonly string sourceAddr = "RSAllies";
    private static readonly string apiUrl = "https://apisms.beem.africa/v1/send";

    public async Task SendMessage(Sms sms)
    {
        using (var httpClient = new HttpClient())
        {
            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{apiKey}:{secretKey}"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));

            var requestBody = new
            {
                source_addr = sourceAddr,
                schedule_time = "",
                encoding = 0,
                message = sms.message,
                recipients = sms.recipients
            };


            var jsonRequestBody = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonRequestBody, Encoding.UTF8, contentType);

            try
            {
                var response = await httpClient.PostAsync(apiUrl, content);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                logger.LogInformation("SMS sent successfully: {0}", JsonConvert.SerializeObject(sms.recipients));
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
            }
        }
    }
}
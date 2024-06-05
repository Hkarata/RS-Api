using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RSAllies.SMS.Contracts.Requests;
using RSAllies.SMS.Contracts.Responses;
using RSAllies.SMS.Extensions;
using System.Net.Http.Json;

namespace RSAllies.SMS.Services;

public class MessageService(HttpClient httpClient, ILogger<MessageService> logger)
{
    public async Task SendMessage(Sms sms)
    {
        var request = JsonConvert.SerializeObject(sms, Formatting.Indented);
        var response = await httpClient.PostAsJsonAsync("/v1/send", request);
        var content = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var successResponse = JsonConvert.DeserializeObject<SmsSuccess>(content);
            logger.LogMessageRequestSuccess(successResponse!);
        }
        else
        {
            var errorResponse = JsonConvert.DeserializeObject<SmsError>(content);
            logger.LogMessageRequestFailed(errorResponse!);
        }
    }
}
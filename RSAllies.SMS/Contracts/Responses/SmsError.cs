using Newtonsoft.Json;

namespace RSAllies.SMS.Contracts.Responses;

public record SmsError
{
    [JsonProperty("code")]
    public int Code { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;
}
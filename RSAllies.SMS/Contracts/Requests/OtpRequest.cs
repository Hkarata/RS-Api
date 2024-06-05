using Newtonsoft.Json;

namespace RSAllies.SMS.Contracts.Requests;

public record OtpRequest
{
    [JsonProperty("appId")]
    public int appId { get; set; }

    [JsonProperty("msisdn")]
    public string msisdn { get; set; } = string.Empty;
}
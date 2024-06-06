using Newtonsoft.Json;

namespace RSAllies.SMS.Contracts.Requests;

public record Sms
{
    [JsonProperty("source_addr")]
    public string source_addr { get; init; } = "RSAllies";

    [JsonProperty("schedule_time")]
    public string schedule_time { get; init; } = string.Empty;

    [JsonProperty("encoding")]
    public string encoding { get; init; } = string.Empty;

    [JsonProperty("message")]
    public string message { get; set; } = string.Empty;

    [JsonProperty("recipients")]
    public List<Recipient>? recipients { get; set; }
}
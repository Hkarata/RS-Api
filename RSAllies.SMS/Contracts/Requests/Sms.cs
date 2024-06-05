using Newtonsoft.Json;

namespace RSAllies.SMS.Contracts.Requests;

public record Sms
{
    [JsonProperty("source_addr")]
    public string SourceAddress { get; init; } = "RSAllies";

    [JsonProperty("schedule_time")]
    public string ScheduleTime { get; init; } = string.Empty;

    [JsonProperty("encoding")]
    public string Encoding { get; init; } = string.Empty;

    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("recipients")]
    public List<Recipient>? Recipients { get; set; }
}
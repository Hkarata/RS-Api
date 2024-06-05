using Newtonsoft.Json;

namespace RSAllies.SMS.Contracts.Responses;

public class SmsSuccess
{
    [JsonProperty("successful")]
    public bool Success { get; set; }

    [JsonProperty("request_id")]
    public int RequestId { get; set; }

    [JsonProperty("code")]
    public int Code { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("valid")]
    public int Valid { get; set; }

    [JsonProperty("invalid")]
    public int Invalid { get; set; }

    [JsonProperty("duplicates")]
    public int Duplicates { get; set; }
}
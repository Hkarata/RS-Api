using Newtonsoft.Json;

namespace RSAllies.SMS.Contracts.Responses;

public record OtpResponse
{
    [JsonProperty("data")]
    public OtpData? Data { get; set; }
}

public record OtpData
{
    [JsonProperty("message")]
    public OtpMessage? Message { get; set; }
}

public record OtpMessage
{
    [JsonProperty("code")]
    public int Code { get; set; }

    [JsonProperty("message")]
    public string Text { get; set; } = string.Empty;
}

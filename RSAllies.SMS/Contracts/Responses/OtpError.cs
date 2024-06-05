using Newtonsoft.Json;

namespace RSAllies.SMS.Contracts.Responses;

public class OtpError
{
    [JsonProperty("code")]
    public int Code { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;
}
using Newtonsoft.Json;

namespace RSAllies.SMS.Contracts.Responses;

public record OtpSuccess
{
    [JsonProperty("data")]
    public Data? Data { get; set; }
}

public record Data
{
    [JsonProperty("pinId")]
    public Guid PinId { get; set; }

    [JsonProperty("message")]
    public Message? Message { get; set; }
}

public record Message
{
    [JsonProperty("code")]
    public int Code { get; set; }

    [JsonProperty("message")]
    public string Text { get; set; } = string.Empty;
}

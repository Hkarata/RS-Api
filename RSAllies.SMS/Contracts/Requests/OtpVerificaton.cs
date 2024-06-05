using Newtonsoft.Json;

namespace RSAllies.SMS.Contracts.Requests;

public record OtpVerification
{
    [JsonProperty("pinId")]
    public Guid PinId { get; set; }

    [JsonProperty("pin")]
    public int Pin { get; set; }
}
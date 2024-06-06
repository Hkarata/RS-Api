using Newtonsoft.Json;
using RSAllies.Shared.Extensions;

namespace RSAllies.SMS.Contracts.Requests;

public record Recipient
{
    [JsonProperty("recipient_id")]
    public string recipient_id { get; set; } = string.Empty;

    [JsonProperty("dest_addr")]
    [PersonalIdentifiableInformation]
    public string dest_addr { get; set; } = string.Empty;
}
using Newtonsoft.Json;
using RSAllies.Shared.Extensions;

namespace RSAllies.SMS.Contracts.Requests;

public record Recipient
{
    [JsonProperty("recipient_id")]
    public int RecipientId { get; set; }

    [JsonProperty("dest_addr")]
    [PersonalIdentifiableInformation]
    public string DestinationAddress { get; set; } = string.Empty;
}
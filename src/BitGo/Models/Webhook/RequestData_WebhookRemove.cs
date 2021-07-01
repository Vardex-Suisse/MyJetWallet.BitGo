using Newtonsoft.Json;
// ReSharper disable InconsistentNaming

namespace MyJetWallet.BitGo.Models.Webhook
{
    public class RequestData_WebhookRemove
    {
        [JsonProperty("type")] public string Type { get; internal set; }
        [JsonProperty("url")] public string Url { get; internal set; }
        [JsonProperty("id")] public string Id { get; internal set; }
    }
}
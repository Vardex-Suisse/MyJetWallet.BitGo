using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Webhook
{
    public class WebhookRemoveResponse
    {
        [JsonProperty("removed")] public int Removed { get; internal set; }
    }
}
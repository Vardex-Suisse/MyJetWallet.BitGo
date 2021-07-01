using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Webhook
{
    public class WebhookInfoList
    {
        [JsonProperty("webhooks")]
        public WebhookInfo[] Webhooks { get; internal set; }
    }
}
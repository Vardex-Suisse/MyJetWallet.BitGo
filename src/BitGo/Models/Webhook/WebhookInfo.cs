using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Webhook
{
    public class WebhookInfo
    { 
        [JsonProperty("id")] public string Id { get; internal set; }
        [JsonProperty("label")] public string Label { get; internal set; }
        [JsonProperty("created")] public string Created { get; internal set; }
        [JsonProperty("coin")] public string Coin { get; internal set; }
        [JsonProperty("type")] public string Type { get; internal set; }
        [JsonProperty("url")] public string Url { get; internal set; }
        [JsonProperty("version")] public string Version { get; internal set; }
        [JsonProperty("numConfirmations")] public string NumConfirmations { get; internal set; }
        [JsonProperty("state")] public string State { get; internal set; }
        [JsonProperty("lastAttempt")] public string LastAttempt { get; internal set; }
        [JsonProperty("failingSince")] public string FailingSince { get; internal set; }
        [JsonProperty("successiveFailedAttempts")] public int SuccessiveFailedAttempts { get; internal set; }
        [JsonProperty("walletId")] public string WalletId { get; internal set; }
        [JsonProperty("allToken")] public bool AllToken { get; internal set; }
    }
}
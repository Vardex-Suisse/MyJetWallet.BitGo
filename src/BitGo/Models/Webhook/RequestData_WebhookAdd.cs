using Newtonsoft.Json;

// ReSharper disable InconsistentNaming

namespace MyJetWallet.BitGo.Models.Webhook
{
    public class RequestData_WebhookAdd
    {
        [JsonProperty("type")] public string Type { get; internal set; }
        [JsonProperty("allToken")] public bool AllToken { get; internal set; }
        [JsonProperty("url")] public string Url { get; internal set; }
        [JsonProperty("label")] public string Label { get; internal set; }
        [JsonProperty("numConfirmations")] public int NumConfirmations { get; internal set; }
        [JsonProperty("listenToFailureStates")] public bool ListenToFailureStates { get; internal set; }
    }
}
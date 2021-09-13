using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Wallet
{
    public class SpendingLimit
    {
        [JsonProperty("coin")] public string Coin { get; internal set; }

        [JsonProperty("timeWindow")] public string TimeWindow { get; internal set; }

        [JsonProperty("limitAmountString")] public string LimitAmountString { get; internal set; }

        [JsonProperty("amountSpentString")] public string AmountSpentString { get; internal set; }
    }
}
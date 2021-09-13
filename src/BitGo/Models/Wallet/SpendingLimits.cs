using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Wallet
{
    public class SpendingLimits
    {
        [JsonProperty("velocityLimitSpending")]
        public SpendingLimit[] Limits { get; internal set; }
    }
}

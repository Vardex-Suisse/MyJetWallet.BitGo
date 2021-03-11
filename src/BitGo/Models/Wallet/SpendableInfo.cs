using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Wallet
{
    public class SpendableInfo
    {
        [JsonProperty("coin")]
        public string Coin { get; internal set; }

        [JsonProperty("maximumSpendable")]
        public string MaximumSpendable { get; internal set; }
    }
}

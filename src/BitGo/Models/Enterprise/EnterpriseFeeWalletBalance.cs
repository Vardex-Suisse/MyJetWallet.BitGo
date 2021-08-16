using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Enterprise
{
    public class EnterpriseFeeWalletBalance
    {
        [JsonProperty("balance")]
        public string Balance { get; internal set; }
    }
}

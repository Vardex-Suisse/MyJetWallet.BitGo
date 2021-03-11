using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Wallet
{
    public class WalletInfoList
    {
        [JsonProperty("coin")]
        public string Coin { get; internal set; }
        
        [JsonProperty("nextBatchPrevId")]
        public string NextBatchPrevId { get; internal set; }
        
        [JsonProperty("wallets")]
        public WalletInfo[] Wallets { get; internal set; }
    }
}

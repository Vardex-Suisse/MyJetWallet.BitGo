using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Shared
{
    public class WalletLimit
    {
        [JsonProperty("ofc")]
        public decimal OFC { get; internal set; }
        
        [JsonProperty("txrp")]
        public decimal TXRP { get; internal set; }
        
        [JsonProperty("xrp")]
        public decimal XRP { get; internal set; }
        
        [JsonProperty("testcoin")]
        public decimal TestCoin { get; internal set; }
    }
}

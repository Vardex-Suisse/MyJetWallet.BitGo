using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Address
{
    public class AddressCoinSpecific
    {
        [JsonProperty("memoId")]
        public string MemoId { get; internal set; }
        
        [JsonProperty("rootAddress")]
        public string RootAddress { get; internal set; }
        
        [JsonProperty("redeemScript")]
        public string RedeemScript { get; internal set; }
    }
}

using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Transfer
{
    public class TransferInfoEntry
    {
        [JsonProperty("address")]
        public string Address { get; internal set; }
        
        [JsonProperty("wallet")]
        public string WalletId { get; internal set; }
        
        [JsonProperty("value")]
        public long Value { get; internal set; }
        
        [JsonProperty("valueString")]
        public string ValueString { get; internal set; }
        
        [JsonProperty("label")]
        public string Label { get; internal set; }
        
        [JsonProperty("isChange")]
        public bool IsChange { get; internal set; }
        
        [JsonProperty("isPayGo")]
        public bool IsPayGo { get; internal set; }
        
        [JsonProperty("token")]
        public string Token { get; internal set; }
    }
}

using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Transfer
{
    [DataContract]
    public class TransferInfoEntry
    {
        [JsonProperty("address"), DataMember(Order = 1)]
        public string Address { get; internal set; }
        
        [JsonProperty("wallet"), DataMember(Order = 2)]
        public string WalletId { get; internal set; }
        
        [JsonProperty("value"), DataMember(Order = 3)]
        public long Value { get; internal set; }
        
        [JsonProperty("valueString"), DataMember(Order = 4)]
        public string ValueString { get; internal set; }
        
        [JsonProperty("label"), DataMember(Order = 5)]
        public string Label { get; internal set; }
        
        [JsonProperty("isChange"), DataMember(Order = 6)]
        public bool IsChange { get; internal set; }
        
        [JsonProperty("isPayGo"), DataMember(Order = 7)]
        public bool IsPayGo { get; internal set; }
        
        [JsonProperty("token"), DataMember(Order = 8)]
        public string Token { get; internal set; }
    }
}

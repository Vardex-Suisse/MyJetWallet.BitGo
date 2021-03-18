using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Transfer
{
    [DataContract]
    public class TransferInfoHistory
    {
        [JsonProperty("date"), DataMember(Order = 1)]
        public DateTime Date { get; internal set; }
        
        [JsonProperty("user"), DataMember(Order = 2)]
        public string UserId { get; internal set; }
        
        [JsonProperty("action"), DataMember(Order = 3)]
        public string Action { get; internal set; }
        
        [JsonProperty("comment"), DataMember(Order = 4)]
        public string Comment { get; internal set; }
    }
}

using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Transfer
{
    [DataContract]
    public class TransferInfoInputOutput
    {
        [JsonProperty("id"), DataMember(Order = 1)]
        public string InputId { get; internal set; }
        
        [JsonProperty("address"), DataMember(Order = 2)]
        public string Address { get; internal set; }
        
        [JsonProperty("value"), DataMember(Order = 3)]
        public long Value { get; internal set; }
        
        [JsonProperty("valueString"), DataMember(Order = 4)]
        public string valueString { get; internal set; }
        
        [JsonProperty("blockHeight"), DataMember(Order = 5)]
        public long BlockHeight { get; internal set; }
        
        [JsonProperty("date"), DataMember(Order = 6)]
        public DateTime Date { get; internal set; }
        
        [JsonProperty("coinbase"), DataMember(Order = 7)]
        public bool Coinbase { get; internal set; }
        
        [JsonProperty("wallet"), DataMember(Order = 8)]
        public string WalletId { get; internal set; }
        
        [JsonProperty("fromWallet"), DataMember(Order = 9)]
        public string FromWalletId { get; internal set; }
        
        [JsonProperty("chain"), DataMember(Order = 10)]
        public long Chain { get; internal set; }
        
        [JsonProperty("index"), DataMember(Order = 11)]
        public long Index { get; internal set; }
        
        [JsonProperty("redeemScript"), DataMember(Order = 12)]
        public string RedeemScript { get; internal set; }
        
        [JsonProperty("witnessScript"), DataMember(Order = 13)]
        public string WitnessScript { get; internal set; }
        
        [JsonProperty("isSegwit"), DataMember(Order = 14)]
        public bool IsSegwit { get; internal set; }
    }
}

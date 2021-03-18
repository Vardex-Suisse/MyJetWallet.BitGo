using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Transfer
{
    [DataContract]
    public class TransferInfo
    {
        [JsonProperty("coin"), DataMember(Order = 1)]
        public string Coin { get; internal set; }
        
        [JsonProperty("id"), DataMember(Order = 2)]
        public string TransferId { get; internal set; }
        
        [JsonProperty("wallet"), DataMember(Order = 3)]
        public string WalletId { get; internal set; }
        
        [JsonProperty("enterprise"), DataMember(Order = 4)]
        public string EnterpriseId { get; internal set; }
        
        [JsonProperty("txid"), DataMember(Order = 5)]
        public string TxId { get; internal set; }
        
        [JsonProperty("height"), DataMember(Order = 6)]
        public int Height { get; internal set; }
        
        [JsonProperty("date"), DataMember(Order = 7)]
        public DateTime Date { get; internal set; }
        
        [JsonProperty("confirmations"), DataMember(Order = 8)]
        public int Confirmations { get; internal set; }
        
        [JsonProperty("type"), DataMember(Order = 9)]
        public TransferType Type { get; internal set; }
        
        [JsonProperty("value"), DataMember(Order = 10)]
        public long Value { get; internal set; }
        
        [JsonProperty("valueString"), DataMember(Order = 11)]
        public string ValueString { get; internal set; }
        
        [JsonProperty("baseValue"), DataMember(Order = 12)]
        public long BaseValue { get; internal set; }
        
        [JsonProperty("baseValueString"), DataMember(Order = 13)]
        public string BaseValueString { get; internal set; }
        
        [JsonProperty("feeString"), DataMember(Order = 14)]
        public string FeeString { get; internal set; }
        
        [JsonProperty("payGoFee"), DataMember(Order = 15)]
        public string PayGoFee { get; internal set; }
        
        [JsonProperty("payGoFeeString"), DataMember(Order = 16)]
        public string PayGoFeeString { get; internal set; }
        
        [JsonProperty("usd"), DataMember(Order = 17)]
        public decimal Usd { get; internal set; }
        
        [JsonProperty("usdRate"), DataMember(Order = 18)]
        public decimal UsdRate { get; internal set; }
        
        [JsonProperty("state"), DataMember(Order = 19)]
        public string State { get; internal set; }
        
        [JsonProperty("tags"), DataMember(Order = 20)]
        public string[] Tags { get; internal set; }
        
        [JsonProperty("history"), DataMember(Order = 21)]
        public TransferInfoHistory[] History { get; internal set; }
        
        [JsonProperty("comment"), DataMember(Order = 22)]
        public string Comment { get; internal set; }
        
        [JsonProperty("vSize"), DataMember(Order = 23)]
        public long VSize { get; internal set; }
        
        //[JsonProperty("coinSpecific")]
        //public CoinSpecificItem CoinSpecific { get; internal set; }
        
        [JsonProperty("sequenceId"), DataMember(Order = 24)]
        public string SequenceId { get; internal set; }
        
        [JsonProperty("entries"), DataMember(Order = 25)]
        public TransferInfoEntry[] Entries { get; internal set; }
        
        [JsonProperty("inputs"), DataMember(Order = 26)]
        public TransferInfoInputOutput[] Inputs { get; internal set; }
        
        [JsonProperty("outputs"), DataMember(Order = 27)]
        public TransferInfoInputOutput[] Outputs { get; internal set; }
    }

    public enum TransferType
    {
        [JsonProperty("send")]
        Send = 0,

        [JsonProperty("receive")]
        Receive = 1
    }
}

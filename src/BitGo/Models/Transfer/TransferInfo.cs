using System;
using System.Collections.Generic;
using MyJetWallet.BitGo.Models.Express;
using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Transfer
{
    public class TransferInfo
    {
        [JsonProperty("coin")]
        public string Coin { get; internal set; }
        
        [JsonProperty("id")]
        public string TransferId { get; internal set; }
        
        [JsonProperty("wallet")]
        public string WalletId { get; internal set; }
        
        [JsonProperty("enterprise")]
        public string EnterpriseId { get; internal set; }
        
        [JsonProperty("txid")]
        public string TxId { get; internal set; }
        
        [JsonProperty("height")]
        public int Height { get; internal set; }
        
        [JsonProperty("date")]
        public DateTime Date { get; internal set; }
        
        [JsonProperty("confirmations")]
        public int Confirmations { get; internal set; }
        
        [JsonProperty("type")]
        public TransferType Type { get; internal set; }
        
        [JsonProperty("value")]
        public long Value { get; internal set; }
        
        [JsonProperty("valueString")]
        public string ValueString { get; internal set; }
        
        [JsonProperty("baseValue")]
        public long BaseValue { get; internal set; }
        
        [JsonProperty("baseValueString")]
        public string BaseValueString { get; internal set; }
        
        [JsonProperty("feeString")]
        public string FeeString { get; internal set; }
        
        [JsonProperty("payGoFee")]
        public string PayGoFee { get; internal set; }
        
        [JsonProperty("payGoFeeString")]
        public string PayGoFeeString { get; internal set; }
        
        [JsonProperty("usd")]
        public decimal Usd { get; internal set; }
        
        [JsonProperty("usdRate")]
        public decimal UsdRate { get; internal set; }
        
        [JsonProperty("state")]
        public string State { get; internal set; }
        
        [JsonProperty("tags")]
        public string[] Tags { get; internal set; }
        
        [JsonProperty("history")]
        public TransferInfoHistory[] History { get; internal set; }
        
        [JsonProperty("comment")]
        public string Comment { get; internal set; }
        
        [JsonProperty("vSize")]
        public long VSize { get; internal set; }
        
        //[JsonProperty("coinSpecific")]
        //public CoinSpecificItem CoinSpecific { get; internal set; }
        
        [JsonProperty("sequenceId")]
        public string SequenceId { get; internal set; }
        
        [JsonProperty("entries")]
        public TransferInfoEntry[] Entries { get; internal set; }
        
        [JsonProperty("inputs")]
        public TransferInfoInputOutput[] Inputs { get; internal set; }
        
        [JsonProperty("outputs")]
        public TransferInfoInputOutput[] Outputs { get; internal set; }

        public class CoinSpecificItem
        {
            [JsonProperty("memo")]
            public MemoType Memo { get; set; }
        }
    }

    public enum TransferType
    {
        [JsonProperty("send")]
        Send = 0,

        [JsonProperty("receive")]
        Receive = 1
    }
}

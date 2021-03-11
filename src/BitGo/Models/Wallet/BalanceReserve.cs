using System;
using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Wallet
{
    public class BalanceReserve
    {
        [JsonProperty("baseFee")]
        public string BaseFee { get; internal set; }
        
        [JsonProperty("baseReserve")]
        public string BaseReserve { get; internal set; }
        
        [JsonProperty("reserve")]
        public string Reserve { get; internal set; }
        
        [JsonProperty("minimumFunding")]
        public string MinimumFunding { get; internal set; }
        
        [JsonProperty("height")]
        public int Height { get; internal set; }
        
        [JsonProperty("date")]
        public DateTime Date { get; internal set; }
    }
}

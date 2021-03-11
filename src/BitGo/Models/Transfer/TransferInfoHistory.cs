using System;
using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Transfer
{
    public class TransferInfoHistory
    {
        [JsonProperty("date")]
        public DateTime Date { get; internal set; }
        
        [JsonProperty("user")]
        public string UserId { get; internal set; }
        
        [JsonProperty("action")]
        public string Action { get; internal set; }
        
        [JsonProperty("comment")]
        public string Comment { get; internal set; }
    }
}

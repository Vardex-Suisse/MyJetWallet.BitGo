using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Wallet
{
    public class BalanceInfo
    {
        [JsonProperty("balance")]
        public long Balance { get; internal set; }
        
        [JsonProperty("balanceString")]
        public string BalanceString { get; internal set; }
        
        [JsonProperty("confirmedBalance")]
        public long? ConfirmedBalance { get; internal set; }
        
        [JsonProperty("confirmedBalanceString")]
        public string ConfirmedBalanceString { get; internal set; }
        
        [JsonProperty("spendableBalance")]
        public long? SpendableBalance { get; internal set; }
        
        [JsonProperty("spendableBalanceString")]
        public string SpendableBalanceString { get; internal set; }
        
        [JsonProperty("tokens")]
        public Dictionary<string, BalanceInfoToken> Tokens { get; internal set; }
    }
}

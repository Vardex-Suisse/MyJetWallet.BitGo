using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Wallet
{
    public class BalanceInfoToken
    {
        [JsonProperty("balanceString")]
        public string BalanceString { get; internal set; }
        
        [JsonProperty("confirmedBalanceString")]
        public string ConfirmedBalanceString { get; internal set; }
        
        [JsonProperty("heldBalanceString")]
        public string HeldBalanceString { get; internal set; }
        
        [JsonProperty("spendableBalanceString")]
        public string SpendableBalanceString { get; internal set; }
        
        [JsonProperty("transferCount")]
        public int TransferCount { get; internal set; }
    }
}

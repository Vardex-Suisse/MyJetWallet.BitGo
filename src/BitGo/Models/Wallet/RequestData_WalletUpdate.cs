using Newtonsoft.Json;
// ReSharper disable InconsistentNaming

namespace MyJetWallet.BitGo.Models.Wallet
{
    public class RequestData_WalletUpdate
    {
        [JsonProperty("label")]
        public string Label { get; internal set; }
        
        [JsonProperty("approvalsRequired")]
        public int ApprovalsRequired { get; internal set; }
        
        [JsonProperty("disableTransactionNotifications")]
        public bool DisableTransactionNotifications { get; internal set; }
    }
}

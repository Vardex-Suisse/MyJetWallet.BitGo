using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Wallet
{
    public class WalletUser
    {
        [JsonProperty("user")]
        public string UserId { get; internal set; }
        
        [JsonProperty("permissions")]
        public string[] Permissions { get; internal set; }
    }
}

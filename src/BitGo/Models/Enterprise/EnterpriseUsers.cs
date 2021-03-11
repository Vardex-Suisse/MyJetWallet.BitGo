using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Enterprise
{

    public class EnterpriseUsers
    {
        [JsonProperty("adminUsers")]
        public EnterpriseUserDetails[] AdminUsers { get; internal set; }
        
        [JsonProperty("nonAdminUsers")]
        public EnterpriseUserDetails[] NonAdminUsers { get; internal set; }
        
        [JsonProperty("walletUsers")]
        public EnterpriseUserDetails[] WalletUsers { get; internal set; }
        
        [JsonProperty("count")]
        public int Count { get; internal set; }
        
        [JsonProperty("incomplete")]
        public bool Incomplete { get; internal set; }
    }
}

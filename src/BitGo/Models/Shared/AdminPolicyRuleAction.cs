using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Shared
{
    public class AdminPolicyRuleAction
    {
        [JsonProperty("type")]
        public string Type { get; internal set; }
        
        [JsonProperty("approvalsRequired")]
        public int ApprovalsRequired { get; internal set; }
        
        [JsonProperty("userIds")]
        public string[] UserIds { get; internal set; }
    }
}

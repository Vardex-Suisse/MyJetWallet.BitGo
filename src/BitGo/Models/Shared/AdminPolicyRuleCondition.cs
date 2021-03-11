using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Shared
{
    public class AdminPolicyRuleCondition
    {
        [JsonProperty("amountString")]
        public string AmountString { get; internal set; }
        
        [JsonProperty("timeWindow")]
        public int TimeWindow { get; internal set; }
    }
}

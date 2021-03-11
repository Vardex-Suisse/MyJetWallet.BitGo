using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.PendingApproval
{

    public class PendingApprovalInfoList
    {
        [JsonProperty("pendingApprovals")]
        public PendingApprovalInfo PendingApprovals { get; internal set; }
    }
}

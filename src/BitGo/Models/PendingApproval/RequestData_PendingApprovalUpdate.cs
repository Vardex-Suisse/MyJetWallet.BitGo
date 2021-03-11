using Newtonsoft.Json;
// ReSharper disable InconsistentNaming

namespace MyJetWallet.BitGo.Models.PendingApproval
{
    public class RequestData_PendingApprovalUpdate
    {
        [JsonProperty("otp")]
        public string OTP { get; internal set; }
        
        [JsonProperty("state")]
        public string State { get; internal set; }
    }
}

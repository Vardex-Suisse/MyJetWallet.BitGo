using Newtonsoft.Json;
// ReSharper disable InconsistentNaming

namespace MyJetWallet.BitGo.Models.User
{
    public class RequestData_UserUnlockSession
    {
        [JsonProperty("duration")]
        public int Duration { get; internal set; }
        
        [JsonProperty("otp")]
        public string OTP { get; internal set; }
    }
}

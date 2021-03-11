using Newtonsoft.Json;
// ReSharper disable InconsistentNaming

namespace MyJetWallet.BitGo.Models.User
{
    public class RequestData_UserLogin
    {
        [JsonProperty("email")]
        public string Email { get; internal set; }
        
        [JsonProperty("password")]
        public string Password { get; internal set; }
        
        [JsonProperty("otp")]
        public string OTP { get; internal set; }
    }
}

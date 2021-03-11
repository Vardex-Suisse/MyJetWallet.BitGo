using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Express
{
    public class VerifyAddressResult
    {
        [JsonProperty("isValid")]
        public bool IsValid { get; set; }
    }
}
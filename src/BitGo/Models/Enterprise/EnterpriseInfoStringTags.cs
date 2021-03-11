using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Enterprise
{

    public class EnterpriseInfoStringTags : EnterpriseInfo
    {
        [JsonProperty("tags")]
        public string[] Tags { get; internal set; }
    }
}

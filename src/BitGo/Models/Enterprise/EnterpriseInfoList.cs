using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Enterprise
{
    public class EnterpriseInfoList
    {
        [JsonProperty("enterprises")]
        public EnterpriseInfoStringTags[] Enterprises { get; internal set; }
    }
}

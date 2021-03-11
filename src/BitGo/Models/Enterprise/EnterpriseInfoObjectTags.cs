using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Enterprise
{

    public class EnterpriseInfoObjectTags : EnterpriseInfo
    {
        [JsonProperty("tags")]
        public EnterpriseTag[] Tags { get; internal set; }
    }

    public class EnterpriseTag
    {
        [JsonProperty("id")]
        public string EnterpriseId { get; internal set; }

        [JsonProperty("name")]
        public string Name { get; internal set; }
    }
}

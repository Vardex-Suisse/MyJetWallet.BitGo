using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Key
{
    public class KeyInfoList
    {
        [JsonProperty("keys")]
        public KeyInfo[] Keys { get; internal set; }
        
        [JsonProperty("limit")]
        public int Limit { get; internal set; }
    }
}

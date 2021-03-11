using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Shared
{
    public class Admin
    {
        [JsonProperty("policy")]
        public AdminPolicy Policy { get; internal set; }
    }
}

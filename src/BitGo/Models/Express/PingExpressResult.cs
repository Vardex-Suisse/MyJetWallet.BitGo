using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Express
{
    public class PingExpressResult
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
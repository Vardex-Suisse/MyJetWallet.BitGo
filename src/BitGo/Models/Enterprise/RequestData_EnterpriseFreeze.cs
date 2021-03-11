using Newtonsoft.Json;
// ReSharper disable InconsistentNaming

namespace MyJetWallet.BitGo.Models.Enterprise
{
    public class RequestData_EnterpriseFreeze
    {
        [JsonProperty("duration")]
        public int Duration  { get; internal set; }
    }
}

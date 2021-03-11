using Newtonsoft.Json;
// ReSharper disable InconsistentNaming

namespace MyJetWallet.BitGo.Models.Enterprise
{
    public class RequestData_EnterpriseRemoveUser
    {
        [JsonProperty("username")]
        public string Username { get; internal set; }
    }
}

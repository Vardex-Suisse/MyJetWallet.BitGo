using Newtonsoft.Json;
// ReSharper disable InconsistentNaming

namespace MyJetWallet.BitGo.Models.Enterprise
{
    public class RequestData_EnterpriseUpdate
    {
        /// <summary>
        /// How many Enterprise Admins are required for action to fire
        /// </summary>
        [JsonProperty("approvalsRequired")]
        public int ApprovalsRequired { get; internal set; }
    }
}

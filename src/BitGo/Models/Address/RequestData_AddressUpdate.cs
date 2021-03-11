using Newtonsoft.Json;
// ReSharper disable InconsistentNaming

namespace MyJetWallet.BitGo.Models.Address
{
    public class RequestData_AddressUpdate
    {
        /// <summary>
        /// Max 250 characters. A human-readable label which should be applied to the new address
        /// </summary>
        [JsonProperty("label")]
        public string Label { get; internal set; }
    }
}

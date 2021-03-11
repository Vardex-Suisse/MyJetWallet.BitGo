using Newtonsoft.Json;
// ReSharper disable InconsistentNaming

namespace MyJetWallet.BitGo.Models.Wallet
{
    public class RequestData_WalletFreeze
    {
        [JsonProperty("duration")]
        public int Duration  { get; internal set; }
    }
}

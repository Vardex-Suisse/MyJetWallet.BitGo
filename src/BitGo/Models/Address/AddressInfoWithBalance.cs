using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Address
{

    public class AddressInfoWithBalance: AddressInfo
    {
        [JsonProperty("balance")]
        public  AddressBalance Balance { get; internal set; }
    }
}

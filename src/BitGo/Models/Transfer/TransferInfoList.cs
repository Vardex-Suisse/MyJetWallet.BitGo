using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Transfer
{
    public class TransferInfoList
    {
        [JsonProperty("transfers")]
        public TransferInfo[] Transfers { get; internal set; }
        
        [JsonProperty("coin")]
        public string Coin { get; internal set; }
        
        [JsonProperty("nextBatchPrevId")]
        public string NextBatchPrevId { get; internal set; }
    }
}

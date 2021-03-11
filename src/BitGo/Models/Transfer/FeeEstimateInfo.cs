using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Transfer
{
    public class FeeEstimateInfo
    {
        [JsonProperty("feeEstimate")]
        public string FeeEstimate { get; internal set; }
        
        [JsonProperty("gasLimitEstimate")]
        public string GasLimitEstimate { get; internal set; }
        
        [JsonProperty("minGasPrice")]
        public string MinGasPrice { get; internal set; }
        
        [JsonProperty("minGasLimit")]
        public string MinGasLimit { get; internal set; }
        
        [JsonProperty("maxGasLimit")]
        public string MaxGasLimit { get; internal set; }
        
        [JsonProperty("feePerKb")]
        public long FeePerKb { get; internal set; }
        
        [JsonProperty("cpfpFeePerKb")]
        public long CpfpFeePerKb { get; internal set; }
        
        [JsonProperty("numBlocks")]
        public long NumBlocks { get; internal set; }
        
        [JsonProperty("confidence")]
        public int Confidence { get; internal set; }
        
        //[JsonProperty("feeByBlockTarget")]
        //public object FeeByBlockTarget { get; internal set; }
    }
}

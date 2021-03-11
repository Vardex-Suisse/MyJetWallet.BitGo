using System;
using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Shared
{
   public class OtpDevice
    {
        [JsonProperty("id")]
        public string OtpDeviceId { get; internal set; }
        
        [JsonProperty("createDate")]
        public DateTime CreateDate { get; internal set; }
        
        [JsonProperty("lastValidatedDate")]
        public DateTime LastValidatedDate { get; internal set; }
        
        [JsonProperty("type")]
        public string Type { get; internal set; }
        
        [JsonProperty("label")]
        public string Label { get; internal set; }
        
        [JsonProperty("verified")]
        public bool Verified { get; internal set; }
    }
}

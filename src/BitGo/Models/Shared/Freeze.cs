using System;
using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Shared
{
    public class Freeze
    {
        [JsonProperty("time")]
        public DateTime Time { get; internal set; }
        
        [JsonProperty("expires")]
        public DateTime Expires { get; internal set; }
    }
}

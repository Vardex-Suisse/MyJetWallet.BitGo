using System.Runtime.Serialization;
using MyJetWallet.BitGo.Models.Transfer;
using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Express
{
    [DataContract]
    public class SendCoinResult
    {
        /// <summary>
        /// New transfer
        /// </summary>
        [JsonProperty("transfer"), DataMember(Order = 1)]
        public TransferInfo Transfer { get; set; }

        /// <summary>
        /// Unique transaction identifier
        /// </summary>
        [JsonProperty("txid"), DataMember(Order = 2)]
        public string Txid { get; set; }

        /// <summary>
        /// Encoded transaction hex(or base64 for XLM)
        /// </summary>
        [JsonProperty("tx"), DataMember(Order = 3)]
        public string Tx { get; set; }

        /// <summary>
        /// Enum: "signed" "signed (suppressed)" "pendingApproval"
        /// Transfer status
        /// </summary>
        [JsonProperty("status"), DataMember(Order = 4)]
        public string Status { get; set; }

        /// <summary>
        /// true = A transaction has been created, but will require approval from admins on the wallet.
        /// false = Transaction successfully sent
        /// </summary>
        [JsonIgnore, DataMember(Order = 5)]
        public bool IsRequireApproval { get; set; }

        public enum StatusEnum
        {
            [JsonProperty("signed")]
            Signed,

            [JsonProperty("signed (suppressed)")]
            SignedSuppressed,

            [JsonProperty("pendingApproval")]
            PendingApproval
        }
    }
}
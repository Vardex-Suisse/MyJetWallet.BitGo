using MyJetWallet.BitGo.Models.Transfer;
using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.Express
{
    public class SendCoinResult
    {
        /// <summary>
        /// New transfer
        /// </summary>
        [JsonProperty("transfer")]
        public TransferInfo Transfer { get; set; }

        /// <summary>
        /// Unique transaction identifier
        /// </summary>
        [JsonProperty("txid")]
        public string Txid { get; set; }

        /// <summary>
        /// Encoded transaction hex(or base64 for XLM)
        /// </summary>
        [JsonProperty("tx")]
        public string Tx { get; set; }

        /// <summary>
        /// Enum: "signed" "signed (suppressed)" "pendingApproval"
        /// Transfer status
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// true = A transaction has been created, but will require approval from admins on the wallet.
        /// false = Transaction successfully sent
        /// </summary>
        [JsonIgnore]
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
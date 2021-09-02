using System;
using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.PendingApproval
{
    public class PendingApprovalInfo
    {
        [JsonProperty("id")]
        public string Id { get; internal set; }
        
        [JsonProperty("coin")]
        public string Coin { get; internal set; }
        
        [JsonProperty("enterprise")]
        public string Enterprise { get; internal set; }
        
        [JsonProperty("walletId")]
        public string WalletId { get; internal set; }
        
        [JsonProperty("walletLabel")]
        public string WalletLabel { get; internal set; }
        
        [JsonProperty("creator")]
        public string Creator { get; internal set; }
        
        [JsonProperty("createDate")]
        public DateTime CreateDate { get; internal set; }
        
        [JsonProperty("info")]
        public Info Info { get; internal set; }
        
        /// <summary>
        /// Enum:"pending" "approved" "rejected"
        /// </summary>
        [JsonProperty("state")]
        public string State { get; internal set; }
        
        [JsonProperty("walletUserIds")]
        public string[] WalletUserIds { get; internal set; }
        
        /// <summary>
        /// number >= 1
        /// </summary>
        [JsonProperty("approvalsRequired")]
        public int ApprovalsRequired { get; internal set; }
    }

    public class Info
    {
        [JsonProperty("type")]
        public string Type { get; internal set; }
        
        [JsonProperty("transactionRequest")]
        public TransactionRequest TransactionRequest { get; internal set; }
        
        [JsonProperty("updateEnterpriseRequest")]
        public UpdateEnterpriseRequest UpdateEnterpriseRequest { get; internal set; }
        
        [JsonProperty("updateApprovalsRequiredRequest")]
        public UpdateApprovalsRequiredRequest UpdateApprovalsRequiredRequest { get; internal set; }
    }

    public class TransactionRequest
    {
        [JsonProperty("comment")]
        public string Comment { get; internal set; }
        [JsonProperty("requestedAmount")]
        public string RequestedAmount { get; internal set; }
        [JsonProperty("sourceWallet")]
        public string SourceWallet { get; internal set; }
        [JsonProperty("recipients")]
        public TransactionRecipient[] Recipients { get; internal set; }
    }

    public class TransactionRecipient
    {
        [JsonProperty("address")]
        public string Address { get; internal set; }
        [JsonProperty("amount")]
        public string Amount { get; internal set; }
        [JsonProperty("data")]
        public string Data { get; internal set; }
    }

    public class UpdateEnterpriseRequest
    {
        [JsonProperty("action")]
        public string Action { get; internal set; }
        
        [JsonProperty("userId")]
        public string UserId { get; internal set; }
        
        [JsonProperty("permissions")]
        public string[] Permissions { get; internal set; }
    }

    public class UpdateApprovalsRequiredRequest
    {
        [JsonProperty("requestedApprovalsRequired")]
        public int RequestedApprovalsRequired { get; internal set; }
    }
}

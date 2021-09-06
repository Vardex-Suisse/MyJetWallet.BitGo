using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models.PendingApproval
{
    [DataContract]
    public class PendingApprovalInfo
    {
        [JsonProperty("id"), DataMember(Order = 1)]
        public string Id { get; internal set; }
        
        [JsonProperty("coin"), DataMember(Order = 2)]
        public string Coin { get; internal set; }
        
        [JsonProperty("enterprise"), DataMember(Order = 3)]
        public string Enterprise { get; internal set; }
        
        [JsonProperty("wallet"), DataMember(Order = 4)]
        public string WalletId { get; internal set; }
        
        [JsonProperty("walletLabel"), DataMember(Order = 5)]
        public string WalletLabel { get; internal set; }
        
        [JsonProperty("creator"), DataMember(Order = 6)]
        public string Creator { get; internal set; }
        
        [JsonProperty("createDate"), DataMember(Order = 7)]
        public DateTime CreateDate { get; internal set; }
        
        [JsonProperty("info"), DataMember(Order = 8)]
        public Info Info { get; internal set; }
        
        /// <summary>
        /// Enum:"pending" "approved" "rejected"
        /// </summary>
        [JsonProperty("state"), DataMember(Order = 9)]
        public string State { get; internal set; }
        
        [JsonProperty("userIds"), DataMember(Order = 10)]
        public string[] WalletUserIds { get; internal set; }
        
        /// <summary>
        /// number >= 1
        /// </summary>
        [JsonProperty("approvalsRequired"), DataMember(Order = 11)]
        public int ApprovalsRequired { get; internal set; }
    }

    [DataContract]
    public class Info
    {
        [JsonProperty("type"), DataMember(Order = 1)]
        public string Type { get; internal set; }
        
        [JsonProperty("transactionRequest"), DataMember(Order = 2)]
        public TransactionRequest TransactionRequest { get; internal set; }
        
        [JsonProperty("updateEnterpriseRequest")]
        public UpdateEnterpriseRequest UpdateEnterpriseRequest { get; internal set; }
        
        [JsonProperty("updateApprovalsRequiredRequest")]
        public UpdateApprovalsRequiredRequest UpdateApprovalsRequiredRequest { get; internal set; }
    }

    [DataContract]
    public class TransactionRequest
    {
        [JsonProperty("comment"), DataMember(Order = 1)]
        public string Comment { get; internal set; }
        [JsonProperty("requestedAmount"), DataMember(Order = 2)]
        public string RequestedAmount { get; internal set; }
        [JsonProperty("sourceWallet"), DataMember(Order = 3)]
        public string SourceWallet { get; internal set; }
        [JsonProperty("recipients"), DataMember(Order = 4)]
        public TransactionRecipient[] Recipients { get; internal set; }
        [JsonProperty("buildParams"), DataMember(Order = 5)]
        public BuildParams BuildParams { get; internal set; }
    }

    [DataContract]
    public class TransactionRecipient
    {
        [JsonProperty("address"), DataMember(Order = 1)]
        public string Address { get; internal set; }
        [JsonProperty("amount"), DataMember(Order = 2)]
        public string Amount { get; internal set; }
        [JsonProperty("data"), DataMember(Order = 3)]
        public string Data { get; internal set; }
    }


    [DataContract]
    public class BuildParams
    {
        [JsonProperty("recipients"), DataMember(Order = 1)]
        public TransactionRecipient[] Recipients { get; internal set; }
        [JsonProperty("sequenceId"), DataMember(Order = 1)]
        public string SequenceId { get; internal set; }
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

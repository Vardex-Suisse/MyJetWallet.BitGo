using System.Collections.Generic;
using MyJetWallet.BitGo.Base;
using MyJetWallet.BitGo.Models;

namespace MyJetWallet.BitGo.Converters
{
    public class PendingApprovalStateConverter : BaseConverter<PendingApprovalState>
    {
        public PendingApprovalStateConverter() : this(true) { }
        public PendingApprovalStateConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<PendingApprovalState, string>> Mapping => new List<KeyValuePair<PendingApprovalState, string>>
        {
            new KeyValuePair<PendingApprovalState, string>(PendingApprovalState.Pending, "pending"),
            new KeyValuePair<PendingApprovalState, string>(PendingApprovalState.AwaitingSignature, "awaitingSignature"),
            new KeyValuePair<PendingApprovalState, string>(PendingApprovalState.PendingBitGoAdminApproval, "pendingBitGoAdminApproval"),
            new KeyValuePair<PendingApprovalState, string>(PendingApprovalState.PendingFinalApproval, "pendingFinalApproval"),
        };
    }
}

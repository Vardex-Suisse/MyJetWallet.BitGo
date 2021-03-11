using System.Collections.Generic;
using MyJetWallet.BitGo.Base;
using MyJetWallet.BitGo.Models;

namespace MyJetWallet.BitGo.Converters
{
    public class PendingApprovalResolveTypeConverter : BaseConverter<PendingApprovalResolveType>
    {
        public PendingApprovalResolveTypeConverter() : this(true) { }
        public PendingApprovalResolveTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<PendingApprovalResolveType, string>> Mapping => new List<KeyValuePair<PendingApprovalResolveType, string>>
        {
            new KeyValuePair<PendingApprovalResolveType, string>(PendingApprovalResolveType.Approved, "approved"),
            new KeyValuePair<PendingApprovalResolveType, string>(PendingApprovalResolveType.Rejected, "rejected"),
        };
    }
}

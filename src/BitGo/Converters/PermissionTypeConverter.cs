using System.Collections.Generic;
using MyJetWallet.BitGo.Base;
using MyJetWallet.BitGo.Models;

namespace MyJetWallet.BitGo.Converters
{
    public class PermissionTypeConverter : BaseConverter<PermissionType>
    {
        public PermissionTypeConverter() : this(true) { }
        public PermissionTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<PermissionType, string>> Mapping => new List<KeyValuePair<PermissionType, string>>
        {
            new KeyValuePair<PermissionType, string>(PermissionType.Admin, "admin"),
            new KeyValuePair<PermissionType, string>(PermissionType.View, "view"),
            new KeyValuePair<PermissionType, string>(PermissionType.Spend, "spend"),
        };
    }
}

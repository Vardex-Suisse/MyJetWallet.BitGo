using System.Collections.Generic;
using MyJetWallet.BitGo.Base;
using MyJetWallet.BitGo.Models;

namespace MyJetWallet.BitGo.Converters
{
    public class KeySourceTypeConverter : BaseConverter<KeySourceType>
    {
        public KeySourceTypeConverter() : this(true) { }
        public KeySourceTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<KeySourceType, string>> Mapping => new List<KeyValuePair<KeySourceType, string>>
        {
            new KeyValuePair<KeySourceType, string>(KeySourceType.Backup, "backup"),
            new KeyValuePair<KeySourceType, string>(KeySourceType.Bitgo, "bitgo"),
            new KeyValuePair<KeySourceType, string>(KeySourceType.Cold, "cold"),
            new KeyValuePair<KeySourceType, string>(KeySourceType.User, "user"),
        };
    }
}

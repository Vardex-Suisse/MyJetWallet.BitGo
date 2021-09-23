using System;
using System.Threading;
using System.Threading.Tasks;
using MyJetWallet.BitGo.Models;
using MyJetWallet.BitGo.Models.Transfer;

namespace MyJetWallet.BitGo
{
    public static class BitGoClientHelper
    {
        public static async Task<TransferInfo> TryGetTransferAsync(this IBitGoApi api, 
            string coin,
            string walletId,
            string transferId = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var transfer = await api.GetTransferAsync(coin, walletId, transferId, cancellationToken);

                return transfer.Data;
            }
            catch (BitGoErrorException ex) when (ex.Error.Code == "Invalid" &&
                                                 ex.Error.Message == "invalid transfer or transaction id")
            {
                return null;
            }
        }
    }
}
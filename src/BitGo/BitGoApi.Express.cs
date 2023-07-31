using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MyJetWallet.BitGo.Models;
using MyJetWallet.BitGo.Models.Express;
using static MyJetWallet.BitGo.Models.Express.SendManyRequestData;

namespace MyJetWallet.BitGo
{
    public partial class BitGoApi
    {
        public async Task<WebCallResult<PingExpressResult>> PingExpressAsync(CancellationToken cancellationToken = default)
        {
            return await this.GetAsync<PingExpressResult>($"{this.EndpointUrl}/pingexpress", cancellationToken);
        }

        public async Task<WebCallResult<VerifyAddressResult>> VerifyAddressAsync(string coin, string address, CancellationToken cancellationToken = default)
        {
            var request = new
            {
                address
            };

            return await this.PostAsync<VerifyAddressResult>($"{this.EndpointUrl}/{coin}/verifyaddress", request, cancellationToken);
        }

        public async Task<WebCallResult<SendCoinResult>> SendManyAsync(
            string coin,
            string walletId,
            SendManyRequestData request,
            CancellationToken cancellationToken = default)
        {
            var resp = await this.PostAsync<SendCoinResult>($"{this.EndpointUrl}/{coin}/wallet/{walletId}/sendmany", request, cancellationToken);

            if (resp.Data != null)
            {
                resp.Data.IsRequireApproval = resp.ResponseStatusCode == HttpStatusCode.Accepted;
            }

            return resp;
        }

        public Task<WebCallResult<SendCoinResult>> SendManyAsync(
            string coin, string walletId, string walletPassphrase,
            List<Recipient> recipients, string sequenceId = null, SendManyRequestData.MemoType memo = null,
            CancellationToken cancellationToken = default)
        {
            var request = new SendManyRequestData()
            {
                WalletPassphrase = walletPassphrase,
                Recipients = recipients,
                SequenceId = sequenceId,
                Memo = memo
            };

            return SendManyAsync(coin, walletId, request, cancellationToken);
        }

        public async Task<WebCallResult<SendCoinResult>> SendCoinsAsync(
            string coin,
            string walletId,
            SendCoinsRequestData request,
            CancellationToken cancellationToken = default)
        {
            var resp = await this.PostAsync<SendCoinResult>($"{this.EndpointUrl}/{coin}/wallet/{walletId}/sendcoins", request, cancellationToken);

            if (resp.Data != null)
            {
                resp.Data.IsRequireApproval = resp.ResponseStatusCode == HttpStatusCode.Accepted;
            }

            return resp;
        }


        public Task<WebCallResult<SendCoinResult>> SendCoinsAsync(
            string coin, string walletId, string walletPassphrase,
            string sequenceId, string amount,
            string address, SendCoinsRequestData.MemoType memo = null,
            CancellationToken cancellationToken = default)
        {
            var request = new SendCoinsRequestData()
            {
                WalletPassphrase = walletPassphrase,
                Address = address,
                Amount = amount,
                SequenceId = sequenceId,
                Memo = memo
            };

            return SendCoinsAsync(coin, walletId, request, cancellationToken);
        }
    }
}
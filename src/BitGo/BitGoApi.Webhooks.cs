using System;
using System.Threading;
using System.Threading.Tasks;
using MyJetWallet.BitGo.Models;
using MyJetWallet.BitGo.Models.Webhook;

namespace MyJetWallet.BitGo
{
    public partial class BitGoApi
    {
        /// <summary>
        /// List webhooks set up on the wallet
        /// </summary>
        /// <param name="coin">A cryptocurrency or token ticker symbol. Example: "btc"</param>
        /// <param name="walletId">Coin Wallet Address. Example: "59cd72485007a239fb00282ed480da1f"</param>
        /// <param name="type">Enum: "transfer" "transaction" "pendingapproval" "address_confirmation" Type of event to listen to (can be transfer or pendingapproval).</param>
        /// <param name="allToken">Default: false Triggers on coin transfers and token transfers for ETH and Stellar.</param>
        /// <param name="url">uri. URL to fire the webhook to.</param>
        /// <param name="label">Label of the new webhook.</param>
        /// <param name="numConfirmations">Number of confirmations before triggering the webhook. If 0 or unspecified, requests will be sent to the callback endpoint when the transfer is first seen and when it is confirmed.</param>
        /// <param name="listenToFailureStates">Whether or not to listen to failed transactions on chain.</param>
        /// <returns></returns>
        public WebCallResult<WebhookInfo> AddWebhook(string coin, string walletId, string type, bool allToken,
            string url, string label,
            int numConfirmations, bool listenToFailureStates,
            CancellationToken cancellationToken = default(CancellationToken)) => this.AddWebhookAsync(coin, walletId,
            type, allToken, url, label, numConfirmations, listenToFailureStates, cancellationToken).Result;

        public async Task<WebCallResult<WebhookInfo>> AddWebhookAsync(string coin, string walletId, string type,
            bool allToken, string url, string label,
            int numConfirmations, bool listenToFailureStates,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new RequestData_WebhookAdd()
            {
                Type = type,
                Url = url,
                Label = label,
                AllToken = allToken,
                NumConfirmations = numConfirmations,
                ListenToFailureStates = listenToFailureStates,
            };

            return await this.PostAsync<WebhookInfo>(
                $"{this.EndpointUrl}/{coin}/wallet/{walletId}/webhooks", data, cancellationToken);
        }

        /// <summary>
        /// List webhooks set up on the wallet
        /// </summary>
        /// <param name="coin">A cryptocurrency or token ticker symbol. Example: "btc"</param>
        /// <param name="walletId">Coin Wallet Address. Example: "59cd72485007a239fb00282ed480da1f"</param>
        /// <returns></returns>
        public WebCallResult<WebhookInfoList> ListWebhooks(
            string coin,
            string walletId,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.ListWebhooksAsync(coin, walletId, cancellationToken).Result;

        public async Task<WebCallResult<WebhookInfoList>> ListWebhooksAsync(
            string coin,
            string walletId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetAsync<WebhookInfoList>($"{this.EndpointUrl}/{coin}/wallet/{walletId}/webhooks",
                cancellationToken);
        }

        /// <summary>
        /// List webhooks set up on the wallet
        /// </summary>
        /// <param name="coin">A cryptocurrency or token ticker symbol. Example: "btc"</param>
        /// <param name="walletId">Coin Wallet Address. Example: "59cd72485007a239fb00282ed480da1f"</param>
        /// <param name="type">Enum: "transfer" "transaction" "pendingapproval" "address_confirmation" Type of event to listen to (can be transfer or pendingapproval).</param>
        /// <param name="url">string uri</param>
        /// <param name="id">string ^[0-9a-f]{32}$ </param>
        /// <returns></returns>
        public WebCallResult<WebhookRemoveResponse> RemoveWebhook(string coin, string walletId, string type, string url,
            string id,
            CancellationToken cancellationToken = default(CancellationToken)) =>
            this.RemoveWebhookAsync(coin, walletId, type, url, id, cancellationToken).Result;

        public async Task<WebCallResult<WebhookRemoveResponse>> RemoveWebhookAsync(string coin, string walletId,
            string type,
            string url, string id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new RequestData_WebhookRemove()
            {
                Type = type,
                Url = url,
                Id = id,
            };

            return await this.DeleteAsync<WebhookRemoveResponse>(
                $"{this.EndpointUrl}/{coin}/wallet/{walletId}/webhooks", data, cancellationToken);
        }
    }
}
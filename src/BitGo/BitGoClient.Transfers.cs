using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MyJetWallet.BitGo.Models;
using MyJetWallet.BitGo.Models.Transfer;

namespace MyJetWallet.BitGo
{
    public partial class BitGoClient
    {
        #region Transfers
        /// <summary>
        /// Returns deposits and withdrawals for a wallet. Transfers are sorted in descending order by height, then id.
        /// </summary>
        /// <param name="coin">Required. Example: "btc"</param>
        /// <param name="walletId">Required. string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678</param>
        /// <param name="allTokens">Include data for all ERC20 tokens</param>
        /// <param name="prevId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678. Return the next batch of results, based on the nextBatchPrevId value from the previous batch.</param>
        /// <param name="state">Enum. The status of this Transfer. Enum:"signed" "unconfirmed" "confirmed" "pendingApproval" "removed" "failed" "rejected"</param>
        /// <param name="searchLabel">Query for Transfers containing this string. Example: "3BAMY2UAudoEwucfwkg8sGR2FJHLPJoWsc"</param>
        /// <param name="limit">Default: 25. Maximum number of results to return. If the result set is truncated, use the nextBatchPrevId value to get the next batch.</param>
        /// <param name="type">Enum:"send" "receive". Filter on sending or receiving Transfers.</param>
        /// <param name="pendingApprovalId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678. Filter for a transfer with a matching pendingApprovalId</param>
        /// <param name="valueGte">Return transfers with a value that is greater than or equal to the given number</param>
        /// <param name="valueLt">Return transfers with a value that is less than the given number</param>
        /// <param name="dateGte">Return transfers with a date that is greater than or equal to the given timestamp</param>
        /// <param name="dateLt">Return transfers with a date that is less than the given timestamp</param>
        /// <param name="address">Example: ["2MvrwRYBAuRtPTiZ5MyKg42Ke55W3fZJfZS"]. Return transfers with elements in entries that have an address field set to this value</param>
        /// <param name="includeHex">Include the raw hex data of the transaction in the response (this may be a large amount of data)</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<TransferInfoList> GetTransfers(
            string coin,
            string walletId,
            bool? allTokens = null,
            string prevId = null,
            string state = null,
            string searchLabel = null,
            int limit = 25,
            string type = null,
            string pendingApprovalId = null,
            long? valueGte = null,
            long? valueLt = null,
            DateTime? dateGte = null,
            DateTime? dateLt = null,
            string address = null,
            bool? includeHex = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetTransfersAsync(
                coin,
                walletId,
                allTokens,
                prevId,
                state,
                searchLabel,
                limit,
                type,
                pendingApprovalId,
                valueGte,
                valueLt,
                dateGte,
                dateLt,
                address,
                includeHex,
                cancellationToken).Result;
        public async Task<WebCallResult<TransferInfoList>> GetTransfersAsync(
            string coin,
            string walletId,
            bool? allTokens = null,
            string prevId = null,
            string state = null,
            string searchLabel = null,
            int limit = 25,
            string type = null,
            string pendingApprovalId = null,
            long? valueGte = null,
            long? valueLt = null,
            DateTime? dateGte = null,
            DateTime? dateLt = null,
            string address = null,
            bool? includeHex = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (limit < 1 || limit > 500)
                return EvaluateError<TransferInfoList>(new ArgumentError("Limit should be between 1-500"));

            var query = this.ConvertToQueryString(new Dictionary<string, object>() {
                { "allTokens", allTokens },
                { "prevId", prevId },
                { "state", state },
                { "searchLabel", searchLabel },
                { "limit", limit },
                { "type", type },
                { "pendingApprovalId", pendingApprovalId },
                { "valueGte", valueGte },
                { "valueLt", valueLt },
                { "dateGte", dateGte },
                { "dateLt", dateLt },
                { "address", address },
                { "includeHex", includeHex },
            });

            return await this.GetAsync<TransferInfoList>($"{this.EndpointUrl}/{coin}/wallet/{walletId}/transfer" + query, cancellationToken);
        }

        /// <summary>
        /// Returns transfer details
        /// </summary>
        /// <param name="coin">Required. Example: "btc"</param>
        /// <param name="walletId">Required. string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678</param>
        /// <param name="transferId">a transfer or transaction id. Example: "f5d8ee39a430901c91a5917b9f2dc19d6d1a0e9cea205b009ca73dd04470b9a5 or 585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<TransferInfo> GetTransfer(
            string coin,
            string walletId,
            string transferId,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetTransferAsync(coin, walletId, transferId, cancellationToken).Result;
        public async Task<WebCallResult<TransferInfo>> GetTransferAsync(
            string coin,
            string walletId,
            string transferId = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetAsync<TransferInfo>($"{this.EndpointUrl}/{coin}/wallet/{walletId}/transfer/{transferId}", cancellationToken);
        }

        /// <summary>
        /// Returns transfer details
        /// </summary>
        /// <param name="coin">Required. Example: "btc"</param>
        /// <param name="walletId">Required. string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678</param>
        /// <param name="sequenceId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<TransferInfo> GetTransferBySequenceId(
            string coin,
            string walletId,
            string sequenceId,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetTransferBySequenceIdAsync(coin, walletId, sequenceId, cancellationToken).Result;
        public async Task<WebCallResult<TransferInfo>> GetTransferBySequenceIdAsync(
            string coin,
            string walletId,
            string sequenceId = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetAsync<TransferInfo>($"{this.EndpointUrl}/{coin}/wallet/{walletId}/transfer/sequenceId/{sequenceId}", cancellationToken);
        }

        /// <summary>
        /// Returns the estimated fee for a transaction. UTXO coins will return a fee per kB, while Account-based coins will return a flat fee estimate
        /// </summary>
        /// <param name="coin">Required. Example: "btc"</param>
        /// <param name="numBlocks">target number of blocks</param>
        /// <param name="recipient">Recipient of the tx to estimate for (only for ETH)</param>
        /// <param name="data">ETH data of the tx to estimate for (only for ETH)</param>
        /// <param name="hop">True if we are estimating for a hop tx, false or unspecified for a wallet tx (only for ETH)</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<FeeEstimateInfo> FeeEstimate(
            string coin,
            int? numBlocks = null,
            string recipient = null,
            string data = null,
            bool? hop = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.FeeEstimateAsync(coin, numBlocks, recipient, data, hop, cancellationToken).Result;
        public async Task<WebCallResult<FeeEstimateInfo>> FeeEstimateAsync(
            string coin,
            int? numBlocks = null,
            string recipient = null,
            string data = null,
            bool? hop = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = this.ConvertToQueryString(new Dictionary<string, object>() {
                { "numBlocks", numBlocks },
                { "recipient", recipient },
                { "data", data },
                { "hop", hop },
            });

            return await this.GetAsync<FeeEstimateInfo>($"{this.EndpointUrl}/{coin}/tx/fee" + query, cancellationToken);
        }
        #endregion
    }
}
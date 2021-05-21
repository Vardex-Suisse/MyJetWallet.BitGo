using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MyJetWallet.BitGo.Models;
using MyJetWallet.BitGo.Models.Address;

namespace MyJetWallet.BitGo
{
    public partial class BitGoClient
    {
        /// <summary>
        /// List receive addresses on a wallet
        /// </summary>
        /// <param name="coin">Required. Example: "btc"</param>
        /// <param name="walletId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678</param>
        /// <param name="labelContains">A case-insensitive regular expression which will be used to filter returned addresses based on their address label.</param>
        /// <param name="limit">integer[1.. 500]. Default: 25. Maximum number of results to return. If the result set is truncated, use the nextBatchPrevId value to get the next batch.</param>
        /// <param name="mine">Default: false. Whether to return only the addresses which the current user has created.</param>
        /// <param name="prevId">Example: "585951a5df8380e0e3063e9f". Return the next batch of results, based on the nextBatchPrevId value from the previous batch.</param>
        /// <param name="chains">Items Enum:0 1 10 11 20 21. Replaces segwit. Mutually exclusive with segwit. Returns only unspents/addresses of the chains passed. If neither chains nor segwit is passed unspents/addresses from all chains are returned.</param>
        /// <param name="sort">Default: 1. Enum:-1 1. Sort order of returned addresses. (1 for ascending, -1 for descending).</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<AddressInfoList> GetAddresses(
            string coin,
            string walletId,
            string labelContains = null,
            int limit = 25,
            bool mine = false,
            string prevId = null,
            string[] chains = null,
            int sort = 1,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetAddressesAsync(coin, walletId, labelContains, limit, mine, prevId, chains, sort, cancellationToken).Result;

        /// <summary>
        /// List receive addresses on a wallet
        /// </summary>
        /// <param name="coin">Required. Example: "btc"</param>
        /// <param name="walletId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678</param>
        /// <param name="labelContains">A case-insensitive regular expression which will be used to filter returned addresses based on their address label.</param>
        /// <param name="limit">integer[1.. 500]. Default: 25. Maximum number of results to return. If the result set is truncated, use the nextBatchPrevId value to get the next batch.</param>
        /// <param name="mine">Default: false. Whether to return only the addresses which the current user has created.</param>
        /// <param name="prevId">Example: "585951a5df8380e0e3063e9f". Return the next batch of results, based on the nextBatchPrevId value from the previous batch.</param>
        /// <param name="chains">Items Enum:0 1 10 11 20 21. Replaces segwit. Mutually exclusive with segwit. Returns only unspents/addresses of the chains passed. If neither chains nor segwit is passed unspents/addresses from all chains are returned.</param>
        /// <param name="sort">Default: 1. Enum:-1 1. Sort order of returned addresses. (1 for ascending, -1 for descending).</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<WebCallResult<AddressInfoList>> GetAddressesAsync(
            string coin,
            string walletId,
            string labelContains = null,
            int limit = 25,
            bool mine = false,
            string prevId = null,
            string[] chains = null,
            int sort = 1,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (limit < 1 || limit > 500)
                return EvaluateError<AddressInfoList>(new ArgumentError("Limit should be between 1-500"));

            if (sort != 1 && sort != -1)
                return EvaluateError<AddressInfoList>(new ArgumentError("Sort should be either 1 or -1"));

            var query = this.ConvertToQueryString(new Dictionary<string, object>() {
                { "labelContains", labelContains },
                { "limit", limit },
                { "mine", mine },
                { "prevId", prevId },
                { "chains", chains },
                { "sort", sort }
            });

            return await this.GetAsync<AddressInfoList>($"{this.EndpointUrl}/{coin}/wallet/{walletId}/addresses" + query, cancellationToken);
        }

        /// <summary>
        /// This API call is used to create a new receive address for your wallet. You may choose to call this API whenever a deposit is made. The BitGo API supports millions of addresses.
        /// Please check the "Coin-Specific Implementation" with regards to fee address management for Ethereum.
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="walletId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="chain">Enum:0 1 10 11 20 21</param>
        /// <param name="label">string Max 250 characters. A human-readable label which should be applied to the new address</param>
        /// <param name="gasPrice">Explicit gas price to use when deploying the forwarder contract (ETH only). If not given, defaults to the current estimated network gas price.</param>
        /// <param name="lowPriority">Default: false. Whether the deployment of the address forwarder contract should use a low priority fee key (ETH only)</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<AddressInfo> CreateAddress(
            string coin,
            string walletId,
            string label,
            int chain,
            string gasPrice,
            bool lowPriority,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.CreateAddressAsync(coin, walletId, label, chain, gasPrice, lowPriority, cancellationToken).Result;

        /// <summary>
        /// This API call is used to create a new receive address for your wallet. You may choose to call this API whenever a deposit is made. The BitGo API supports millions of addresses.
        /// Please check the "Coin-Specific Implementation" with regards to fee address management for Ethereum.
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="walletId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="chain">Enum:0 1 10 11 20 21</param>
        /// <param name="label">string Max 250 characters. A human-readable label which should be applied to the new address</param>
        /// <param name="gasPrice">Explicit gas price to use when deploying the forwarder contract (ETH only). If not given, defaults to the current estimated network gas price.</param>
        /// <param name="lowPriority">Default: false. Whether the deployment of the address forwarder contract should use a low priority fee key (ETH only)</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<WebCallResult<AddressInfo>> CreateAddressAsync(
            string coin,
            string walletId,
            string label,
            int chain = 0,
            string gasPrice = default,
            bool lowPriority = false,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new RequestData_AddressCreate
            {
                Chain = chain,
                Label = label,
                LowPriority = lowPriority
            };

            if (gasPrice != default)
            {
                data.GasPrice = gasPrice;
            }

            return await this.PostAsync<AddressInfo>($"{this.EndpointUrl}/{coin}/wallet/{walletId}/address", data, cancellationToken);
        }

        /// <summary>
        /// Gets a receive address on a wallet
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="walletId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="addressOrId">Address or Address Id which will be used for information lookup</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<AddressInfoWithBalance> GetAddress(
            string coin,
            string walletId,
            string addressOrId,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetAddressAsync(coin, walletId, addressOrId, cancellationToken).Result;

        /// <summary>
        /// Gets a receive address on a wallet
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="walletId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="addressOrId">Address or Address Id which will be used for information lookup</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<WebCallResult<AddressInfoWithBalance>> GetAddressAsync(
            string coin,
            string walletId,
            string addressOrId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetAsync<AddressInfoWithBalance>($"{this.EndpointUrl}/{coin}/wallet/{walletId}/address/{addressOrId}", cancellationToken);
        }

        /// <summary>
        /// Update a receive address on a wallet
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="walletId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="addressOrId">Address or Address Id which will be used for information lookup</param>
        /// <param name="label">string Max 250 characters. A human-readable label which should be applied to the new address</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<AddressInfoWithBalance> UpdateAddress(
            string coin,
            string walletId,
            string addressOrId,
            string label,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.UpdateAddressAsync(coin, walletId, addressOrId, label, cancellationToken).Result;

        /// <summary>
        /// Update a receive address on a wallet
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="walletId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="addressOrId">Address or Address Id which will be used for information lookup</param>
        /// <param name="label">string Max 250 characters. A human-readable label which should be applied to the new address</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<WebCallResult<AddressInfoWithBalance>> UpdateAddressAsync(
            string coin,
            string walletId,
            string addressOrId,
            string label,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new RequestData_AddressUpdate
            {
                Label = label,
            };

            return await this.PutAsync<AddressInfoWithBalance>($"{this.EndpointUrl}/{coin}/wallet/{walletId}/address/{addressOrId}", data, cancellationToken);
        }
    }
}
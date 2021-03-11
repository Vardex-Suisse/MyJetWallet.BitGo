using System;
using System.Threading;
using System.Threading.Tasks;
using MyJetWallet.BitGo.Models;
using MyJetWallet.BitGo.Models.Address;
using MyJetWallet.BitGo.Models.Enterprise;
using MyJetWallet.BitGo.Models.Express;
using MyJetWallet.BitGo.Models.Key;
using MyJetWallet.BitGo.Models.PendingApproval;
using MyJetWallet.BitGo.Models.Shared;
using MyJetWallet.BitGo.Models.Transfer;
using MyJetWallet.BitGo.Models.User;
using MyJetWallet.BitGo.Models.Wallet;

namespace MyJetWallet.BitGo
{
    public interface IBitGoClient
    {
        Task<WebCallResult<PingExpressResult>> PingExpressAsync(CancellationToken cancellationToken = default);
        Task<WebCallResult<VerifyAddressResult>> VerifyAddressAsync(string coin, string address, CancellationToken cancellationToken = default);

        Task<WebCallResult<SendCoinResult>> SendCoinsAsync(
            string coin,
            string walletId,
            SendCoinsRequestData request,
            CancellationToken cancellationToken = default);

        Task<WebCallResult<SendCoinResult>> SendCoinsAsync(
            string coin, string walletId, string walletPassphrase,
            string sequenceId, string amount, 
            string address, MemoType memo = null, 
            CancellationToken cancellationToken = default);

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
        WebCallResult<TransferInfoList> GetTransfers(
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
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<TransferInfoList>> GetTransfersAsync(
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
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Returns transfer details
        /// </summary>
        /// <param name="coin">Required. Example: "btc"</param>
        /// <param name="walletId">Required. string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678</param>
        /// <param name="transferId">a transfer or transaction id. Example: "f5d8ee39a430901c91a5917b9f2dc19d6d1a0e9cea205b009ca73dd04470b9a5 or 585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<TransferInfo> GetTransfer(
            string coin,
            string walletId,
            string transferId,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<TransferInfo>> GetTransferAsync(
            string coin,
            string walletId,
            string transferId = null,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Returns transfer details
        /// </summary>
        /// <param name="coin">Required. Example: "btc"</param>
        /// <param name="walletId">Required. string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678</param>
        /// <param name="sequenceId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<TransferInfo> GetTransferBySequenceId(
            string coin,
            string walletId,
            string sequenceId,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<TransferInfo>> GetTransferBySequenceIdAsync(
            string coin,
            string walletId,
            string sequenceId = null,
            CancellationToken cancellationToken = default(CancellationToken));

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
        WebCallResult<FeeEstimateInfo> FeeEstimate(
            string coin,
            int? numBlocks = null,
            string recipient = null,
            string data = null,
            bool? hop = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<FeeEstimateInfo>> FeeEstimateAsync(
            string coin,
            int? numBlocks = null,
            string recipient = null,
            string data = null,
            bool? hop = null,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Returns the associated user
        /// </summary>
        /// <param name="userId">The user ID, email address, or me for the currently authenticated user</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<UserInfoResponse> GetUser(
            string userId,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<UserInfoResponse>> GetUserAsync(
            string userId,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Creates a short-lived (1 hour) access token for use with the API. The token must be specified to subsequent API calls via the Authorization HTTP header:
        /// Authorization: Bearer 9b72c68ef394f5146f0f3efc1feafb7a971752cb00e79fafcfd8c1d2db83639c
        /// We don't recommend using this endpoint for scripting. The preferred approach is to create a long-lived token in the web UI (see the Developer Options section in User Settings).
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <param name="otp">Second factor authentication token</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<UserLoginInfo> Login(
            string email,
            string password,
            string otp = "",
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<UserLoginInfo>> LoginAsync(
            string email,
            string password,
            string otp = "",
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Disables an access token
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<object> Logout(
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<object>> LogoutAsync(
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Returns the session associated with access token passed via the Authorization header.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<UserSession> GetSession(
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<UserSession>> GetSessionAsync(
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Locks the current user session. This disallows operations that require an unlocked token, such as sending a transaction.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<UserSession> LockSession(
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<UserSession>> LockSessionAsync(
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Unlocks thes current user session. This allows operations that require an unlocked token, such as sending a transaction. Call this endpoint when an API returns a 401 response with the needsUnlock body parameter set to true.
        /// </summary>
        /// <param name="otp">Second factor authentication token</param>
        /// <param name="duration">integer [ 1 .. 3600 ]</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<UserSession> UnlockSession(
            string otp = "",
            int duration = 600,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<UserSession>> UnlockSessionAsync(
            string otp = "",
            int duration = 600,
            CancellationToken cancellationToken = default(CancellationToken));

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
        WebCallResult<AddressInfoList> GetAddresses(
            string coin,
            string walletId,
            string labelContains = null,
            int limit = 25,
            bool mine = false,
            string prevId = null,
            string[] chains = null,
            int sort = 1,
            CancellationToken cancellationToken = default(CancellationToken));

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
        Task<WebCallResult<AddressInfoList>> GetAddressesAsync(
            string coin,
            string walletId,
            string labelContains = null,
            int limit = 25,
            bool mine = false,
            string prevId = null,
            string[] chains = null,
            int sort = 1,
            CancellationToken cancellationToken = default(CancellationToken));

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
        WebCallResult<AddressInfo> CreateAddress(
            string coin,
            string walletId,
            int chain,
            string label,
            string gasPrice,
            bool lowPriority,
            CancellationToken cancellationToken = default(CancellationToken));

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
        Task<WebCallResult<AddressInfo>> CreateAddressAsync(
            string coin,
            string walletId,
            int chain,
            string label,
            string gasPrice,
            bool lowPriority,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets a receive address on a wallet
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="walletId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="addressOrId">Address or Address Id which will be used for information lookup</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<AddressInfoWithBalance> GetAddress(
            string coin,
            string walletId,
            string addressOrId,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets a receive address on a wallet
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="walletId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="addressOrId">Address or Address Id which will be used for information lookup</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<WebCallResult<AddressInfoWithBalance>> GetAddressAsync(
            string coin,
            string walletId,
            string addressOrId,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Update a receive address on a wallet
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="walletId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="addressOrId">Address or Address Id which will be used for information lookup</param>
        /// <param name="label">string Max 250 characters. A human-readable label which should be applied to the new address</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<AddressInfoWithBalance> UpdateAddress(
            string coin,
            string walletId,
            string addressOrId,
            string label,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Update a receive address on a wallet
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="walletId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="addressOrId">Address or Address Id which will be used for information lookup</param>
        /// <param name="label">string Max 250 characters. A human-readable label which should be applied to the new address</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<WebCallResult<AddressInfoWithBalance>> UpdateAddressAsync(
            string coin,
            string walletId,
            string addressOrId,
            string label,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets all pending approvals
        /// </summary>
        /// <param name="enterpriseId">Filter by enterprise. string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="walletId">Filter by wallet. string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="coin">Filter by coin. Example: ["btc"]</param>
        /// <param name="state">Items Enum:"pending" "awaitingSignature" "pendingBitGoAdminApproval" "pendingFinalApproval". Filter by state. The default behavior is to return objects where state is awaitingSignature, pending, pendingBitGoAdminApproval, or pendingFinalApproval.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<PendingApprovalInfoList> GetPendingApprovals(
            string enterpriseId = null,
            string walletId = null,
            string[] coin = null,
            string[] state = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<PendingApprovalInfoList>> GetPendingApprovalsAsync(
            string enterpriseId = null,
            string walletId = null,
            string[] coin = null,
            string[] state = null,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets a pending approval
        /// </summary>
        /// <param name="pendingId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<PendingApprovalInfo> GetPendingApproval(
            string pendingId,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<PendingApprovalInfo>> GetPendingApprovalAsync(
            string pendingId,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Updates the state of a pending approval to either approved or rejected. Pending approvals are designed to be managed through our web UI. Requests made using an authentication token are not allowed to approve requests. Instead of using pending approvals we recommend creating a webhook policy to do automated approval and denial of transactions.
        /// </summary>
        /// <param name="pendingId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="otp"></param>
        /// <param name="state">approved or rejected</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<PendingApprovalInfo> UpdatePendingApproval(
            string pendingId,
            string otp,
            string state,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<PendingApprovalInfo>> UpdatePendingApprovalAsync(
            string pendingId,
            string otp,
            string state,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Lists Keys
        /// </summary>
        /// <param name="coin">Required. Example: "btc"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<KeyInfoList> GetKeys(
            string coin,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<KeyInfoList>> GetKeysAsync(
            string coin,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Creates Key
        /// </summary>
        /// <param name="coin">Required. Example: "btc"</param>
        /// <param name="enterpriseId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="source">Enum:"backup" "bitgo" "cold" "user"</param>
        /// <param name="encryptedPrv">Private part of this key pair, encrypted with a passphrase that only the client knows. Required for all sources except "bitgo".</param>
        /// <param name="newFeeAddress">Create a new keychain instead of fetching enterprise key (only for Ethereum)</param>
        /// <param name="pub">Public part of this key pair. Required for all sources except "bitgo".</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<KeyInfo> CreateKey(
            string coin,
            string enterpriseId,
            string source = "bitgo",
            string encryptedPrv = "",
            bool newFeeAddress = false,
            string pub = "",
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<KeyInfo>> CreateKeyAsync(
            string coin,
            string enterpriseId = "",
            string source = "bitgo",
            string encryptedPrv = "",
            bool newFeeAddress = false,
            string pub = "",
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets a Key
        /// </summary>
        /// <param name="coin">Required. Example: "btc"</param>
        /// <param name="keyId">Required. string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<KeyInfo> GetKey(
            string coin,
            string keyId,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<KeyInfo>> GetAddressAsync(
            string coin,
            string keyId,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// List wallets
        /// </summary>
        /// <param name="coin">Required. Example: "btc". Lowercase coin symbol</param>
        /// <param name="limit">integer [ 1 .. 500 ]. Default: 25. Maximum number of results to return. If the result set is truncated, use the nextBatchPrevId value to get the next batch.</param>
        /// <param name="allTokens">Example: true. Include data for all ERC20 tokens</param>
        /// <param name="prevId">Example: "585951a5df8380e0e3063e9f". Return the next batch of results, based on the nextBatchPrevId value from the previous batch.</param>
        /// <param name="searchLabel">Example: "3BAMY2UAudoEwucfwkg8sGR2FJHLPJoWsc". Query for Transfers containing this string</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<WalletInfoList> GetWallets(
            string coin,
            int limit = 25,
            bool allTokens = true,
            string prevId = null,
            string searchLabel = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<WalletInfoList>> GetWalletsAsync(
            string coin,
            int limit = 25,
            bool allTokens = true,
            string prevId = null,
            string searchLabel = null,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets wallet details
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="walletId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="allTokens">Include data for all ERC20 tokens</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<WalletInfo> GetWallet(
            string coin,
            string walletId = null,
            bool? allTokens = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<WalletInfo>> GetWalletsAsync(
            string coin,
            string walletId = null,
            bool? allTokens = null,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets wallet details by address
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="address">Coin Wallet Address. Example: "2MvrwRYBAuRtPTiZ5MyKg42Ke55W3fZJfZS"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<WalletInfo> GetWalletByAddress(
            string coin,
            string address,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<WalletInfo>> GetWalletByAddressAsync(
            string coin,
            string address,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// This method is for advanced API users and allows manual creation and specification of keys. In the SDK or BitGo Express, Generate Wallet is the simpler and highly recommended method to create a wallet. Another option is to create your wallets in our UI.
        /// This API creates a new wallet for the user or enterprise.The keys to use with the new wallet(passed in the 'keys' parameter) must be registered with BitGo prior to using this API.
        /// BitGo currently only supports 2-of-3 (e.g.m= 2 and n = 3) wallets. The third key, and only the third key, must be a BitGo key. The first key is by convention the user key, with its encrypted xprv stored on BitGo.
        /// Ethereum and XRP wallets can only be created under an enterprise. Pass in the id of the enterprise to associate the wallet with. Your enterprise id can be seen by clicking on the "Manage Organization" link in the enterprise dropdown. Using the Add Wallet API, you can create a wallet using either the enterprise fee address(used by default for all wallets in the enterprise), or a unique fee address(created manually with the Keychains API). Pass the desired key as the third key ID in the 'keys' array.In either case, the fee address must be funded before creating the wallet.
        /// You cannot generate a wallet by passing in an ERC20 token as the coin. ERC20 tokens share Ethereum wallets and it is not possible to create a wallet specific to one token.
        /// BitGo Ethereum wallet is a smart-contract implementing multi-signature scheme. Because contracts itself can not initiate transactions, fee addresses are used for this purpose.Ethereum transactions initiated by a given address, are confirmed by the network in order of creation, so one lower fee transaction can potentially delay all subsequent transactions.To help lower network fee costs, two fee addresses are provided.
        /// feeAddress is a main fee address usable for all operations. lowPriorityFeeAddress is a secondary fee address that can be used to pay lower fee for Create Address operations without risking delaying subsequent higher-priority transactions initiated by main fee address.
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="label"></param>
        /// <param name="enterpriseId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="stellarUsername">Username for the user's Stellar address. It's case insensitive, and it can't be changed after it's set.</param>
        /// <param name="isCold"></param>
        /// <param name="keys"></param>
        /// <param name="bitgoKeySignature">a signature of the bitgo pub key using the user key (useful for change address verification)</param>
        /// <param name="backupKeySignature">a signature of the backup pub key using the user key (useful for change address verification)</param>
        /// <param name="requiredSignaturesNumber">Number of signatures required. This value must be 2 for hot wallets, 1 for ofc wallets, and not specified for custodial wallets.</param>
        /// <param name="providedKeysNumber">Number of keys provided. This value must be 3 for hot wallets, 1 for ofc wallets, and not specified for custodial wallets.</param>
        /// <param name="tags"></param>
        /// <param name="type">Enum:"custodial" "custodialPaired". The type describes who owns the keys to the wallet and how they are stored. custodial means that this wallet is a cold wallet where BitGo owns the keys. Only customers of the BitGo Trust can create this kind of wallet. custodialPaired means that this is a hot wallet that is owned by the customer but it will be linked to a cold (custodial) wallet where BitGo owns the keys. This option is only available to customers of BitGo Inc.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<WalletInfo> AddWallet(
            string coin,
            string label,
            string enterpriseId,
            string stellarUsername,
            bool isCold,
            string[] keys,
            string bitgoKeySignature,
            string backupKeySignature,
            int requiredSignaturesNumber,
            int providedKeysNumber,
            string[] tags,
            string type,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<WalletInfo>> AddWalletAsync(
            string coin,
            string label,
            string enterpriseId,
            string stellarUsername,
            bool isCold,
            string[] keys,
            string bitgoKeySignature,
            string backupKeySignature,
            int requiredSignaturesNumber,
            int providedKeysNumber,
            string[] tags,
            string type,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Updates wallet
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="address">Coin Wallet Address. Example: "2MvrwRYBAuRtPTiZ5MyKg42Ke55W3fZJfZS"</param>
        /// <param name="walletLabel"></param>
        /// <param name="approvalsRequired">integer >= 1</param>
        /// <param name="disableTransactionNotifications"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<WalletInfo> UpdateWallet(
            string coin,
            string walletId,
            string walletLabel,
            int approvalsRequired,
            bool disableTransactionNotifications,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<WalletInfo>> UpdateWalletAsync(
            string coin,
            string walletId,
            string walletLabel,
            int approvalsRequired,
            bool disableTransactionNotifications,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// You will no longer see or have access to this wallet, but it remains accessible to other wallet users.
        /// If you are the only user on the wallet, the wallet must have a 0 balance.
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="address">Coin Wallet Address. Example: "2MvrwRYBAuRtPTiZ5MyKg42Ke55W3fZJfZS"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<WalletInfo> DeleteWallet(
            string coin,
            string walletId,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<WalletInfo>> DeleteWalletAsync(
            string coin,
            string walletId,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// After a user has accepted a wallet share, they become a party on a wallet and the wallet share is considered “complete”. In order to revoke the share after they have accepted, you can remove the user from the wallet.
        /// This operation requires approval by another wallet administrator if there is more than a single administrator on a wallet.
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="walletId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="userId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<WalletInfo> RemoveUserFromWallet(
            string coin,
            string walletId,
            string userId,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<WalletInfo>> RemoveUserFromWalletAsync(
            string coin,
            string walletId,
            string userId,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Freezes the enterprise for limited time
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="walletId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="durationInSeconds">seconds to freeze the enterprise for</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<Freeze> FreezeWallet(
            string coin,
            string walletId,
            int durationInSeconds,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<Freeze>> FreezeWalletAsync(
            string coin,
            string walletId,
            int durationInSeconds,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get the total balance, confirmed balance, and spendable balance of the wallets of a certain coin type
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="allTokens">Include data for all ERC20 tokens</param>
        /// <param name="enterpriseId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<BalanceInfo> GetBalances(
            string coin,
            bool allTokens = true,
            string enterpriseId = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<BalanceInfo>> GetBalancesAsync(
            string coin,
            bool allTokens = true,
            string enterpriseId = null,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Returns information about reserve requirements for an account. Currently only available for Stellar.
        /// </summary>
        /// <param name="coin">Enum:"txlm" "xlm"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<BalanceReserve> GetBalanceReserve(
            string coin,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<BalanceReserve>> GetBalanceReserveAsync(
            string coin,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Returns unspect transaction outputs for a wallet
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="walletId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="limit">integer [ 1 .. 500 ]. Default: 25. Maximum number of results to return. If the result set is truncated, use the nextBatchPrevId value to get the next batch.</param>
        /// <param name="minValue">number >= 0. Minimum value of each unspent in base units(e.g.satoshis)</param>
        /// <param name="maxValue">number >= 0. Maximum value of each unspent in base units(e.g.satoshis)</param>
        /// <param name="minConfirms">integer >= 0. Minimum number of confirmation for the collected inputs</param>
        /// <param name="minHeight">number >= 0. Minimum block height of the unspents</param>
        /// <param name="prevId">Example: "585951a5df8380e0e3063e9f". Return the next batch of results, based on the nextBatchPrevId value from the previous batch.</param>
        /// <param name="target">integer >= 0. Combined target value of the unspents</param>
        /// <param name="chains">Array of string. Items Enum:0 1 10 11 20 21. Replaces segwit. Mutually exclusive with segwit. Returns only unspents/addresses of the chains passed. If neither chains nor segwit is passed unspents/addresses from all chains are returned.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<UnspentsInfoList> GetUnspents(
            string coin,
            string walletId,
            int limit = 25,
            decimal? minValue = null,
            decimal? maxValue = null,
            int? minConfirms = null,
            int? minHeight = null,
            string prevId = null,
            int? target = null,
            string[] chains = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<UnspentsInfoList>> GetUnspentsAsync(
            string coin,
            string walletId,
            int limit = 25,
            decimal? minValue = null,
            decimal? maxValue = null,
            int? minConfirms = null,
            int? minHeight = null,
            string prevId = null,
            int? target = null,
            string[] chains = null,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Returns the maximum amount that can be spent with a single transaction on the wallet.
        /// The maximum spendable amount can differ from a wallet’s total balance. A transaction can only use up to 200 unspents. Wallets that have more than 200 unspents cannot spend the full balance in one transaction. Additionally, the value returned for the maximum spendable amount accounts for the current fee level by deducting the estimated fees. The amount will only be calculated based on the unspents that fit the parameters passed.
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="walletId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="limit">integer [ 1 .. 500 ]. Default: 25. Maximum number of results to return. If the result set is truncated, use the nextBatchPrevId value to get the next batch.</param>
        /// <param name="allTokens">Include data for all ERC20 tokens</param>
        /// <param name="enforceMinConfirmsForChange">Enforces minConfirms on change inputs</param>
        /// <param name="feeRate">integer >= 0</param>
        /// <param name="maxFeeRate">integer >= 0</param>
        /// <param name="minConfirms">integer >= 0. Minimum number of confirmation for the collected inputs</param>
        /// <param name="minHeight">number >= 0. Minimum block height of the unspents</param>
        /// <param name="minValue">number >= 0. Minimum value of each unspent in base units (e.g. satoshis)</param>
        /// <param name="maxValue">number >= 0. Maximum value of each unspent in base units (e.g. satoshis)</param>
        /// <param name="numBlocks">integer [ 1 .. 1000 ]. Default: 2. Sets the target estimated number of blocks for a confirmation</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<SpendableInfo> GetMaximumSpendable(
            string coin,
            string walletId,
            int limit = 25,
            bool? allTokens = null,
            bool? enforceMinConfirmsForChange = null,
            int? feeRate = null,
            int? maxFeeRate = null,
            int? minConfirms = null,
            decimal? minHeight = null,
            decimal? minValue = null,
            decimal? maxValue = null,
            int numBlocks = 2,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<SpendableInfo>> GetMaximumSpendableAsync(
            string coin,
            string walletId,
            int limit = 25,
            bool? allTokens = null,
            bool? enforceMinConfirmsForChange = null,
            int? feeRate = null,
            int? maxFeeRate = null,
            int? minConfirms = null,
            decimal? minHeight = null,
            decimal? minValue = null,
            decimal? maxValue = null,
            int numBlocks = 2,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets enterprises list
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<EnterpriseInfoList> GetEnterprises(CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<EnterpriseInfoList>> GetEnterprisesAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// CreateS an enterprise
        /// </summary>
        /// <param name="name">Required. Name of Organization</param>
        /// <param name="enterpriseUrl">Required. Url of Organization</param>
        /// <param name="emergencyPhone">Phone number for emergencies</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<EnterpriseInfoObjectTags> CreateEnterprise(
            string name,
            string enterpriseUrl,
            string emergencyPhone = "",
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<EnterpriseInfoObjectTags>> CreateEnterpriseAsync(
            string name,
            string enterpriseUrl,
            string emergencyPhone = "",
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets an enterprise details
        /// </summary>
        /// <param name="enterpriseId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<EnterpriseInfoObjectTags> GetEnterprise(
            string enterpriseId,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<EnterpriseInfoObjectTags>> GetEnterpriseAsync(
            string enterpriseId,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Updates number of required approvals  for an enterprise
        /// </summary>
        /// <param name="enterpriseId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="approvalsRequired"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<PendingApprovalInfo> UpdateEnterprise(
            string enterpriseId,
            int approvalsRequired,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<PendingApprovalInfo>> UpdateEnterpriseAsync(
            string enterpriseId,
            int approvalsRequired,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Lists enterprise users
        /// </summary>
        /// <param name="enterpriseId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="allowInactiveAdmins">Whether inactive admins should be returned as well</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<EnterpriseUsers> GetEnterpriseUsers(
            string enterpriseId,
            bool allowInactiveAdmins,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<EnterpriseUsers>> GetEnterpriseUsersAsync(
            string enterpriseId,
            bool allowInactiveAdmins,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Adds User To Enterprise
        /// </summary>
        /// <param name="enterpriseId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="username">The Username of the User that should be added to the Enterprise</param>
        /// <param name="usernames"></param>
        /// <param name="permission">Value:"admin"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<PendingApprovalInfo> AddUserToEnterprise(
            string enterpriseId,
            string username,
            string[] usernames,
            string permission,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<PendingApprovalInfo>> AddUserToEnterpriseAsync(
            string enterpriseId,
            string username,
            string[] usernames,
            string permission,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Removes User From Enterprise
        /// </summary>
        /// <param name="enterpriseId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="username">The Username of the User that should be removed from the Enterprise</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<PendingApprovalInfo> RemoveUserFromEnterprise(
            string enterpriseId,
            string username,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<PendingApprovalInfo>> RemoveUserFromEnterpriseAsync(
            string enterpriseId,
            string username,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Freezes the enterprise for limited time
        /// </summary>
        /// <param name="enterpriseId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="durationInSeconds">seconds to freeze the enterprise for</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<Freeze> FreezeEnterprise(
            string enterpriseId,
            int durationInSeconds,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<Freeze>> FreezeEnterpriseAsync(
            string enterpriseId,
            int durationInSeconds,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets enterprise's wallet limits
        /// </summary>
        /// <param name="enterpriseId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="coin">Filter by coin(s). Example: ["btc"]</param>
        /// <param name="isCustodial">Whether custodial limits should be returned</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        WebCallResult<EnterpriseWalletLimits> GetEnterpriseWalletLimits(
            string enterpriseId,
            string[] coin = null,
            bool? isCustodial = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<WebCallResult<EnterpriseWalletLimits>> GetEnterpriseWalletLimitsAsync(
            string enterpriseId,
            string[] coin = null,
            bool? isCustodial = null,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
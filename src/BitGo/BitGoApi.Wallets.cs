using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MyJetWallet.BitGo.Models;
using MyJetWallet.BitGo.Models.Shared;
using MyJetWallet.BitGo.Models.Wallet;

namespace MyJetWallet.BitGo
{
    public partial class BitGoApi
    {
        #region Wallets
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
        public WebCallResult<WalletInfoList> GetWallets(
            string coin,
            int limit = 25,
            bool allTokens = true,
            string prevId = null,
            string searchLabel = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetWalletsAsync(coin, limit, allTokens, prevId, searchLabel, cancellationToken).Result;
        public async Task<WebCallResult<WalletInfoList>> GetWalletsAsync(
            string coin,
            int limit = 25,
            bool allTokens = true,
            string prevId = null,
            string searchLabel = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (limit < 1 || limit > 500)
                return EvaluateError<WalletInfoList>(new ArgumentError("Limit should be between 1-500"));

            var query = this.ConvertToQueryString(new Dictionary<string, object>() {
                { "prevId", prevId },
                { "limit", limit },
                { "allTokens", allTokens },
                { "searchLabel", searchLabel }
            });

            return await this.GetAsync<WalletInfoList>($"{this.EndpointUrl}/{coin}/wallet" + query, cancellationToken);
        }

        /// <summary>
        /// Gets wallet details
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="walletId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="allTokens">Include data for all ERC20 tokens</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<WalletInfo> GetWallet(
            string coin,
            string walletId = null,
            bool? allTokens = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetWalletsAsync(coin, walletId, allTokens, cancellationToken).Result;
        public async Task<WebCallResult<WalletInfo>> GetWalletsAsync(
            string coin,
            string walletId = null,
            bool? allTokens = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = this.ConvertToQueryString(new Dictionary<string, object>() {
                { "allTokens", allTokens },
            });

            return await this.GetAsync<WalletInfo>($"{this.EndpointUrl}/{coin}/wallet/{walletId}" + query, cancellationToken);
        }

        /// <summary>
        /// Gets wallet details by address
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="address">Coin Wallet Address. Example: "2MvrwRYBAuRtPTiZ5MyKg42Ke55W3fZJfZS"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<WalletInfo> GetWalletByAddress(
            string coin,
            string address,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetWalletByAddressAsync(coin, address, cancellationToken).Result;
        public async Task<WebCallResult<WalletInfo>> GetWalletByAddressAsync(
            string coin,
            string address,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetAsync<WalletInfo>($"{this.EndpointUrl}/{coin}/wallet/address/{address}", cancellationToken);
        }









        /*
        public WebCallResult<WalletInfo> GenerateWallet(
            string coin,
            string label,
            string passphrase,
            string userKey ,
            string backupXpub ,
            string backupXpubProvider ,
            string enterpriseId ,
            bool disableTransactionNotifications ,
            string passcodeEncryptionCode ,
            string coldDerivationSeed ,
            int gasPrice,
            bool disableKRSEmail,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GenerateWalletAsync(
            coin, 
            label,
            passphrase, 
            userKey, 
            backupXpub, 
            backupXpubProvider, 
            enterpriseId, 
            disableTransactionNotifications,
            passcodeEncryptionCode,
            coldDerivationSeed,
            gasPrice,
            disableKRSEmail,
            cancellationToken).Result;
        public async Task<WebCallResult<WalletInfo>> GenerateWalletAsync(
            string coin,
            string label,
            string passphrase,
            string userKey,
            string backupXpub,
            string backupXpubProvider,
            string enterpriseId,
            bool disableTransactionNotifications,
            string passcodeEncryptionCode,
            string coldDerivationSeed,
            int gasPrice,
            bool disableKRSEmail,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new RequestData_GenerateWallet
            {
                Label = label,
                Passphrase = passphrase,
                UserKey = userKey,
                BackupXpub = backupXpub,
                BackupXpubProvider = backupXpubProvider,
                EnterpriseId = enterpriseId,
                DisableTransactionNotifications = disableTransactionNotifications,
                PasscodeEncryptionCode = passcodeEncryptionCode,
                ColdDerivationSeed = coldDerivationSeed,
                GasPrice = gasPrice,
                DisableKRSEmail = disableKRSEmail
            };

            return await this.PostAsync<WalletInfo>($"{this.EndpointUrl}/{coin}/wallet/generate", data, cancellationToken);
        }
        */



        // TODO: Need Live Test
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
        public WebCallResult<WalletInfo> AddWallet(
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
            CancellationToken cancellationToken = default(CancellationToken))
            => this.AddWalletAsync(
                coin,
                label,
                enterpriseId,
                stellarUsername,
                isCold,
                keys,
                bitgoKeySignature,
                backupKeySignature,
                requiredSignaturesNumber,
                providedKeysNumber,
                tags,
                type,
                cancellationToken).Result;
        public async Task<WebCallResult<WalletInfo>> AddWalletAsync(
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
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new RequestData_AddWallet
            {
                CoinSpecific = new Dictionary<string, Dictionary<string, string>>
                {
                    {
                        "xlm", new Dictionary<string, string>
                        {
                            { "stellarUsername", stellarUsername}
                        }
                    },
                    {
                        "txlm", new Dictionary<string, string>
                        {
                            { "stellarUsername", stellarUsername}
                        }
                    },
                },
                EnterpriseId = enterpriseId,
                IsCold = isCold,
                Keys = keys,
                KeySignatures = new Dictionary<string, string>
                {
                    {"bitgo",bitgoKeySignature },
                    {"backup",backupKeySignature }
                },
                Label = label,
                RequiredSignaturesNumber = requiredSignaturesNumber,
                ProvidedKeysNumber = providedKeysNumber,
                Tags = tags,
                Type = type
            };

            return await this.PostAsync<WalletInfo>($"{this.EndpointUrl}/{coin}/wallet", data, cancellationToken);
        }

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
        public WebCallResult<WalletInfo> UpdateWallet(
            string coin,
            string walletId,
            string walletLabel,
            int approvalsRequired,
            bool disableTransactionNotifications,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.UpdateWalletAsync(coin, walletId, walletLabel, approvalsRequired, disableTransactionNotifications, cancellationToken).Result;
        public async Task<WebCallResult<WalletInfo>> UpdateWalletAsync(
            string coin,
            string walletId,
            string walletLabel,
            int approvalsRequired,
            bool disableTransactionNotifications,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new RequestData_WalletUpdate
            {
                Label = walletLabel,
                ApprovalsRequired = approvalsRequired,
                DisableTransactionNotifications = disableTransactionNotifications
            };

            return await this.PutAsync<WalletInfo>($"{this.EndpointUrl}/{coin}/wallet/{walletId}", data, cancellationToken);
        }

        /// <summary>
        /// You will no longer see or have access to this wallet, but it remains accessible to other wallet users.
        /// If you are the only user on the wallet, the wallet must have a 0 balance.
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="address">Coin Wallet Address. Example: "2MvrwRYBAuRtPTiZ5MyKg42Ke55W3fZJfZS"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<WalletInfo> DeleteWallet(
            string coin,
            string walletId,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.DeleteWalletAsync(coin, walletId, cancellationToken).Result;
        public async Task<WebCallResult<WalletInfo>> DeleteWalletAsync(
            string coin,
            string walletId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.DeleteAsync<WalletInfo>($"{this.EndpointUrl}/{coin}/wallet/{walletId}", null, cancellationToken);
        }

        // TODO: Need Live Test
        /// <summary>
        /// After a user has accepted a wallet share, they become a party on a wallet and the wallet share is considered “complete”. In order to revoke the share after they have accepted, you can remove the user from the wallet.
        /// This operation requires approval by another wallet administrator if there is more than a single administrator on a wallet.
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="walletId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="userId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<WalletInfo> RemoveUserFromWallet(
            string coin,
            string walletId,
            string userId,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.RemoveUserFromWalletAsync(coin, walletId, userId, cancellationToken).Result;
        public async Task<WebCallResult<WalletInfo>> RemoveUserFromWalletAsync(
            string coin,
            string walletId,
            string userId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.DeleteAsync<WalletInfo>($"{this.EndpointUrl}/{coin}/wallet/{walletId}/user/{userId}", null, cancellationToken);
        }

        /// <summary>
        /// Freezes the enterprise for limited time
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="walletId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="durationInSeconds">seconds to freeze the enterprise for</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<Freeze> FreezeWallet(
            string coin,
            string walletId,
            int durationInSeconds,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.FreezeWalletAsync(coin, walletId, durationInSeconds, cancellationToken).Result;
        public async Task<WebCallResult<Freeze>> FreezeWalletAsync(
            string coin,
            string walletId,
            int durationInSeconds,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new RequestData_WalletFreeze
            {
                Duration = durationInSeconds,
            };

            return await this.PostAsync<Freeze>($"{this.EndpointUrl}/{coin}/wallet/{walletId}/freeze", data, cancellationToken);
        }

        /// <summary>
        /// Get the total balance, confirmed balance, and spendable balance of the wallets of a certain coin type
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="allTokens">Include data for all ERC20 tokens</param>
        /// <param name="enterpriseId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<BalanceInfo> GetBalances(
            string coin,
            bool allTokens = true,
            string enterpriseId = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetBalancesAsync(coin, allTokens, enterpriseId, cancellationToken).Result;
        public async Task<WebCallResult<BalanceInfo>> GetBalancesAsync(
            string coin,
            bool allTokens = true,
            string enterpriseId = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = this.ConvertToQueryString(new Dictionary<string, object>() {
                { "allTokens", allTokens },
                { "enterprise", enterpriseId },
            });

            return await this.GetAsync<BalanceInfo>($"{this.EndpointUrl}/{coin}/wallet/balances" + query, cancellationToken);
        }

        /// <summary>
        /// Returns information about reserve requirements for an account. Currently only available for Stellar.
        /// </summary>
        /// <param name="coin">Enum:"txlm" "xlm"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<BalanceReserve> GetBalanceReserve(
            string coin,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetBalanceReserveAsync(coin, cancellationToken).Result;
        public async Task<WebCallResult<BalanceReserve>> GetBalanceReserveAsync(
            string coin,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetAsync<BalanceReserve>($"{this.EndpointUrl}/{coin}/requiredReserve", cancellationToken);
        }

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
        public WebCallResult<UnspentsInfoList> GetUnspents(
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
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetUnspentsAsync(coin, walletId, limit, minValue, maxValue, minConfirms, minHeight, prevId, target, chains, cancellationToken).Result;
        public async Task<WebCallResult<UnspentsInfoList>> GetUnspentsAsync(
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
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (limit < 1 || limit > 500)
                return EvaluateError<UnspentsInfoList>(new ArgumentError("Limit should be between 1-500"));

            var query = this.ConvertToQueryString(new Dictionary<string, object>() {
                { "limit", limit },
                { "minValue", minValue },
                { "maxValue", maxValue },
                { "minConfirms", minConfirms },
                { "minHeight", minHeight },
                { "prevId", prevId },
                { "target", target },
                { "chains", chains },
            });

            return await this.GetAsync<UnspentsInfoList>($"{this.EndpointUrl}/{coin}/wallet/{walletId}/unspents" + query, cancellationToken);
        }

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
        public WebCallResult<SpendableInfo> GetMaximumSpendable(
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
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetMaximumSpendableAsync(coin, walletId, limit, allTokens, enforceMinConfirmsForChange, feeRate, maxFeeRate, minConfirms, minHeight, minValue, maxValue, numBlocks, cancellationToken).Result;
        public async Task<WebCallResult<SpendableInfo>> GetMaximumSpendableAsync(
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
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (limit < 1 || limit > 500)
                return EvaluateError<SpendableInfo>(new ArgumentError("Limit should be between 1-500"));

            if (numBlocks < 1 || numBlocks > 1000)
                return EvaluateError<SpendableInfo>(new ArgumentError("Limit should be between 1-1000"));

            var query = this.ConvertToQueryString(new Dictionary<string, object>() {
                { "limit", limit },
                { "allTokens", allTokens },
                { "enforceMinConfirmsForChange", enforceMinConfirmsForChange },
                { "feeRate", feeRate },
                { "maxFeeRate", maxFeeRate },
                { "minConfirms", minConfirms },
                { "minHeight", minHeight },
                { "minValue", minValue },
                { "maxValue", maxValue },
                { "numBlocks", numBlocks },
            });

            return await this.GetAsync<SpendableInfo>($"{this.EndpointUrl}/{coin}/wallet/{walletId}/maximumSpendable" + query, cancellationToken);
        }
        
        

        /// <summary>
        /// Returns the wallet's currently configured spending limits and the current amount spent during the periods defined by the spending limits.
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="walletId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<SpendingLimits> GetSpendingLimitsForWallet(
            string coin,
            string walletId,
            CancellationToken cancellationToken = default)
            => GetSpendingLimitsForWalletAsync(coin, walletId, cancellationToken).Result;
        public async Task<WebCallResult<SpendingLimits>> GetSpendingLimitsForWalletAsync(
            string coin,
            string walletId,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<SpendingLimits>($"{EndpointUrl}/{coin}/wallet/{walletId}/spending", cancellationToken);
        }

        // buradan devam

        // Build a transaction
        // Initiate a transaction
        // Send a half-signed transaction
        #endregion
    }
}
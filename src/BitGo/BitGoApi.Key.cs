using System.Threading;
using System.Threading.Tasks;
using MyJetWallet.BitGo.Models;
using MyJetWallet.BitGo.Models.Key;

namespace MyJetWallet.BitGo
{
    public partial class BitGoApi
    {
        #region Key
        /// <summary>
        /// Lists Keys
        /// </summary>
        /// <param name="coin">Required. Example: "btc"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<KeyInfoList> GetKeys(
            string coin,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetKeysAsync(coin, cancellationToken).Result;
        public async Task<WebCallResult<KeyInfoList>> GetKeysAsync(
            string coin,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetAsync<KeyInfoList>($"{this.EndpointUrl}/{coin}/key", cancellationToken);
        }

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
        public WebCallResult<KeyInfo> CreateKey(
            string coin,
            string enterpriseId,
            string source = "bitgo",
            string encryptedPrv = "",
            bool newFeeAddress = false,
            string pub = "",
            CancellationToken cancellationToken = default(CancellationToken))
            => this.CreateKeyAsync(coin, enterpriseId, source, encryptedPrv, newFeeAddress, pub, cancellationToken).Result;
        public async Task<WebCallResult<KeyInfo>> CreateKeyAsync(
            string coin,
            string enterpriseId = "",
            string source = "bitgo",
            string encryptedPrv = "",
            bool newFeeAddress = false,
            string pub = "",
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new RequestData_KeyCreate
            {
                Source = source,
                EnterpriseId = enterpriseId,
                EncryptedPrv = encryptedPrv,
                UseNewFeeAddress = newFeeAddress,
                Pub = pub,
            };

            return await this.PostAsync<KeyInfo>($"{this.EndpointUrl}/{coin}/key", data, cancellationToken);
        }

        /// <summary>
        /// Gets a Key
        /// </summary>
        /// <param name="coin">Required. Example: "btc"</param>
        /// <param name="keyId">Required. string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<KeyInfo> GetKey(
            string coin,
            string keyId,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetAddressAsync(coin, keyId, cancellationToken).Result;
        public async Task<WebCallResult<KeyInfo>> GetAddressAsync(
            string coin,
            string keyId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetAsync<KeyInfo>($"{this.EndpointUrl}/{coin}/key/{keyId}", cancellationToken);
        }
        #endregion
    }
}
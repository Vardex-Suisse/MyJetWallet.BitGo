using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using MyJetWallet.BitGo.Models;
using Newtonsoft.Json;

namespace MyJetWallet.BitGo
{
    public partial class BitGoClient: IDisposable, IBitGoClient
    {
        public const string MainPublicApi = "https://www.bitgo.com/api/v2";
        public const string TestPublicApi = "https://test.bitgo.com/api/v2";

        #region Properties
        public string EndpointUrl { get; private set; }
        
        public SecureString AccessToken { get; private set; }

        public bool ThrowThenErrorResponse { get; set; } = true;

        private HttpClient _httpClient;
        private DateTime _lastHttpSetupTime;
        private HttpClient _lastHttpClient;
        private readonly object _gate = new object();
        #endregion

        #region CTOR
        public BitGoClient(string accessToken, BitGoNetwork network = BitGoNetwork.Main)
        {
            this.EndpointUrl = network == BitGoNetwork.Main ? MainPublicApi : TestPublicApi;
            this.SetAccessToken(accessToken);
        }

        public BitGoClient(string accessToken, string apiRootUrl)
        {
            if (string.IsNullOrEmpty(apiRootUrl))
                throw new ArgumentException("api url cannot be empty", nameof(apiRootUrl));

            if (apiRootUrl.Last() != '/')
                apiRootUrl += '/';

            apiRootUrl += "api/v2";

            this.EndpointUrl = apiRootUrl;
            this.SetAccessToken(accessToken);
        }
        #endregion

        #region Access Token
        public void SetAccessToken(string accessToken)
        {
            if (!string.IsNullOrEmpty(accessToken))
            {
                this.AccessToken = accessToken.StringToSecureString();
            }
        }
        #endregion

        #region Private Methods

        private HttpClient GetHttpClient()
        {
            lock (_gate)
            {
                if (_httpClient == null || (DateTime.UtcNow - _lastHttpSetupTime).TotalSeconds > 120)
                {
                    SetupHttpClient();
                }

                return _httpClient;
            }
        }

        private void SetupHttpClient()
        {
            var handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false
            };

            var client = new HttpClient(handler);

            client.BaseAddress = new Uri(this.EndpointUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", this.AccessToken.SecureStringToString());

            _lastHttpClient?.Dispose();
            _lastHttpClient = _httpClient;
            _httpClient = client;
            _lastHttpSetupTime = DateTime.UtcNow;

            Console.WriteLine($"[{DateTime.UtcNow:O}]Bitgo setup new HttpClient");
        }

        private bool CheckForErrors(string data)
        {
            return data.Contains("\"error\"") && data.Contains("\"name\"") && data.Contains("\"requestId\"") ||
                   data.Contains("\"error\"") && data.Contains("\"message\"");
        }

        private string ConvertToQueryString(Dictionary<string, object> nvc)
        {
            var array = nvc
                .Where(keyValue => keyValue.Value != null)
                .Select(keyValue => new KeyValuePair<string, string>(keyValue.Key, keyValue.Value.ConvertValueToString()))
                .Select(keyValue => $"{WebUtility.UrlEncode(keyValue.Key)}={WebUtility.UrlEncode(keyValue.Value)}")
                .ToArray();
            return array.Any() ? "?" + string.Join("&", array) : string.Empty;
        }

        private async Task<WebCallResult<T>> GetAsync<T>(string url, CancellationToken cancellationToken = default(CancellationToken))
        {
            var client = this.GetHttpClient();
            var response = await client.GetAsync($"{url}", cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            // Return
            return this.EvaluateResponse<T>(response, content);
        }

        private async Task<WebCallResult<T>> PostAsync<T>(string url, object obj = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var client = GetHttpClient();
            var data = JsonConvert.SerializeObject(obj ?? new object());
            var response = await client.PostAsync($"{url}", new StringContent(data, Encoding.UTF8, "application/json"), cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            // Return
            return this.EvaluateResponse<T>(response, content);
        }

        private async Task<WebCallResult<T>> PutAsync<T>(string url, object obj = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var client = GetHttpClient();
            var data = JsonConvert.SerializeObject(obj ?? new object());
            var response = await client.PutAsync($"{url}", new StringContent(data, Encoding.UTF8, "application/json"), cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            // Return
            return this.EvaluateResponse<T>(response, content);
        }

        private async Task<WebCallResult<T>> DeleteAsync<T>(string url, object obj = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var client = GetHttpClient();
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{url}");
            var data = JsonConvert.SerializeObject(obj ?? new object());
            request.Content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request, cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            // Return
            return this.EvaluateResponse<T>(response, content);
        }

        private WebCallResult<T> EvaluateResponse<T>(HttpResponseMessage response, string content)
        {
            // Error
            Error error = null;

            if (string.IsNullOrEmpty(content))
            {
                error = new Error("000", "Empty Response", "");
                ThrowErrorExceptionIfEnabled(error);
                return new WebCallResult<T>(response, default(T), error);
            }

            if (this.CheckForErrors(content))
            {
                error = JsonConvert.DeserializeObject<Error>(content);
                ThrowErrorExceptionIfEnabled(error);
                return new WebCallResult<T>(response, default(T), error);
            }

            if (response.StatusCode == HttpStatusCode.OK || 
                response.StatusCode == HttpStatusCode.Accepted ||
                response.StatusCode == HttpStatusCode.PartialContent)
            {
                return new WebCallResult<T>(response, JsonConvert.DeserializeObject<T>(content), null);
            }

            error = new Error(((int)response.StatusCode).ToString(), response.StatusCode.ToString(), "");

            ThrowErrorExceptionIfEnabled(error);
            return new WebCallResult<T>(response, default(T), error);

        }

        private void ThrowErrorExceptionIfEnabled(Error error)
        {
            if (error != null && ThrowThenErrorResponse)
            {
                throw new BitGoErrorException(error);
            }
        }

        private WebCallResult<T> EvaluateError<T>(Error error)
        {
            if (ThrowThenErrorResponse)
            {
                throw new BitGoErrorException(error);
            }

            return WebCallResult<T>.CreateErrorResult(error);
        }

        #endregion

        public void Dispose()
        {
            _httpClient?.Dispose();
            _lastHttpClient?.Dispose();
            AccessToken?.Dispose();
        }
    }
}

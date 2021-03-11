using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MyJetWallet.BitGo.Models;
using MyJetWallet.BitGo.Models.User;

namespace MyJetWallet.BitGo
{
    public partial class BitGoClient
    {
        #region User
        /// <summary>
        /// Returns the associated user
        /// </summary>
        /// <param name="userId">The user ID, email address, or me for the currently authenticated user</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<UserInfoResponse> GetUser(
            string userId,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetUserAsync(userId, cancellationToken).Result;
        public async Task<WebCallResult<UserInfoResponse>> GetUserAsync(
            string userId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = this.ConvertToQueryString(new Dictionary<string, object> {
                { "id",userId},
            });

            return await this.GetAsync<UserInfoResponse>($"{this.EndpointUrl}/user/{userId}" + query, cancellationToken);
        }

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
        public WebCallResult<UserLoginInfo> Login(
            string email,
            string password,
            string otp = "",
            CancellationToken cancellationToken = default(CancellationToken))
            => this.LoginAsync(email, password, otp, cancellationToken).Result;
        public async Task<WebCallResult<UserLoginInfo>> LoginAsync(
            string email,
            string password,
            string otp = "",
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new RequestData_UserLogin
            {
                Email = email,
                Password = password,
                OTP = otp,
            };

            return await this.PostAsync<UserLoginInfo>($"{this.EndpointUrl}/user/login", data, cancellationToken);
        }

        /// <summary>
        /// Disables an access token
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<object> Logout(
            CancellationToken cancellationToken = default(CancellationToken))
            => this.LogoutAsync(cancellationToken).Result;
        public async Task<WebCallResult<object>> LogoutAsync(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetAsync<object>($"{this.EndpointUrl}/user/logout", cancellationToken);
        }

        /// <summary>
        /// Returns the session associated with access token passed via the Authorization header.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<UserSession> GetSession(
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetSessionAsync(cancellationToken).Result;
        public async Task<WebCallResult<UserSession>> GetSessionAsync(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetAsync<UserSession>($"{this.EndpointUrl}/user/session", cancellationToken);
        }

        /// <summary>
        /// Locks the current user session. This disallows operations that require an unlocked token, such as sending a transaction.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<UserSession> LockSession(
            CancellationToken cancellationToken = default(CancellationToken))
            => this.LockSessionAsync(cancellationToken).Result;
        public async Task<WebCallResult<UserSession>> LockSessionAsync(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.PostAsync<UserSession>($"{this.EndpointUrl}/user/lock", cancellationToken);
        }

        /// <summary>
        /// Unlocks thes current user session. This allows operations that require an unlocked token, such as sending a transaction. Call this endpoint when an API returns a 401 response with the needsUnlock body parameter set to true.
        /// </summary>
        /// <param name="otp">Second factor authentication token</param>
        /// <param name="duration">integer [ 1 .. 3600 ]</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<UserSession> UnlockSession(
            string otp = "",
            int duration = 600,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.UnlockSessionAsync(otp, duration, cancellationToken).Result;
        public async Task<WebCallResult<UserSession>> UnlockSessionAsync(
            string otp = "",
            int duration = 600,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new RequestData_UserUnlockSession
            {
                OTP = otp,
                Duration = duration
            };

            return await this.PostAsync<UserSession>($"{this.EndpointUrl}/user/unlock", data, cancellationToken);
        }
        #endregion
    }
}
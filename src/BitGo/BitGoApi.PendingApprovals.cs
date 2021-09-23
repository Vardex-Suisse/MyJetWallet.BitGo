using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MyJetWallet.BitGo.Models;
using MyJetWallet.BitGo.Models.PendingApproval;

namespace MyJetWallet.BitGo
{
    public partial class BitGoApi
    {
        #region Pending Approvals
        // TODO: Needs to Live Test
        /// <summary>
        /// Gets all pending approvals
        /// </summary>
        /// <param name="enterpriseId">Filter by enterprise. string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="walletId">Filter by wallet. string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="coin">Filter by coin. Example: ["btc"]</param>
        /// <param name="state">Items Enum:"pending" "awaitingSignature" "pendingBitGoAdminApproval" "pendingFinalApproval". Filter by state. The default behavior is to return objects where state is awaitingSignature, pending, pendingBitGoAdminApproval, or pendingFinalApproval.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<PendingApprovalInfoList> GetPendingApprovals(
            string enterpriseId = null,
            string walletId = null,
            string[] coin = null,
            string[] state = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetPendingApprovalsAsync(enterpriseId, walletId, coin, state, cancellationToken).Result;
        public async Task<WebCallResult<PendingApprovalInfoList>> GetPendingApprovalsAsync(
            string enterpriseId = null,
            string walletId = null,
            string[] coin = null,
            string[] state = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var dict = new Dictionary<string, object>();
            dict.Add("enterpriseId", enterpriseId);
            dict.Add("walletId", walletId);
            if (coin != null) dict.Add("coin", "[\"" + string.Join("\",\"", coin) + "\"]");
            if (state != null) dict.Add("state", "[\"" + string.Join("\",\"", state) + "\"]");
            var query = this.ConvertToQueryString(dict);

            return await this.GetAsync<PendingApprovalInfoList>($"{this.EndpointUrl}/pendingApprovals" + query, cancellationToken);
        }

        // TODO: Needs to Live Test
        /// <summary>
        /// Gets a pending approval
        /// </summary>
        /// <param name="pendingId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<PendingApprovalInfo> GetPendingApproval(
            string pendingId,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetPendingApprovalAsync(pendingId, cancellationToken).Result;
        public async Task<WebCallResult<PendingApprovalInfo>> GetPendingApprovalAsync(
            string pendingId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetAsync<PendingApprovalInfo>($"{this.EndpointUrl}/pendingApprovals/{pendingId}", cancellationToken);
        }

        // TODO: Needs to Live Test
        /// <summary>
        /// Updates the state of a pending approval to either approved or rejected. Pending approvals are designed to be managed through our web UI. Requests made using an authentication token are not allowed to approve requests. Instead of using pending approvals we recommend creating a webhook policy to do automated approval and denial of transactions.
        /// </summary>
        /// <param name="pendingId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="otp"></param>
        /// <param name="state">approved or rejected</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<PendingApprovalInfo> UpdatePendingApproval(
            string pendingId,
            string otp,
            string state,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.UpdatePendingApprovalAsync(pendingId, otp, state, cancellationToken).Result;
        public async Task<WebCallResult<PendingApprovalInfo>> UpdatePendingApprovalAsync(
            string pendingId,
            string otp,
            string state,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new RequestData_PendingApprovalUpdate
            {
                OTP = otp,
                State = state
            };

            return await this.PutAsync<PendingApprovalInfo>($"{this.EndpointUrl}/pendingApprovals/{pendingId}", data, cancellationToken);
        }
        #endregion
    }
}
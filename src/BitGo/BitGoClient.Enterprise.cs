using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MyJetWallet.BitGo.Models;
using MyJetWallet.BitGo.Models.Enterprise;
using MyJetWallet.BitGo.Models.PendingApproval;
using MyJetWallet.BitGo.Models.Shared;

namespace MyJetWallet.BitGo
{
    public partial class BitGoClient
    {
        #region Enterprise
        /// <summary>
        /// Gets enterprises list
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<EnterpriseInfoList> GetEnterprises(CancellationToken cancellationToken = default(CancellationToken)) => this.GetEnterprisesAsync(cancellationToken).Result;
        public async Task<WebCallResult<EnterpriseInfoList>> GetEnterprisesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetAsync<EnterpriseInfoList>($"{this.EndpointUrl}/enterprise", cancellationToken);
        }

        /// <summary>
        /// CreateS an enterprise
        /// </summary>
        /// <param name="name">Required. Name of Organization</param>
        /// <param name="enterpriseUrl">Required. Url of Organization</param>
        /// <param name="emergencyPhone">Phone number for emergencies</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<EnterpriseInfoObjectTags> CreateEnterprise(
            string name,
            string enterpriseUrl,
            string emergencyPhone = "",
            CancellationToken cancellationToken = default(CancellationToken))
            => this.CreateEnterpriseAsync(name, enterpriseUrl, emergencyPhone, cancellationToken).Result;
        public async Task<WebCallResult<EnterpriseInfoObjectTags>> CreateEnterpriseAsync(
            string name,
            string enterpriseUrl,
            string emergencyPhone = "",
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new RequestData_EnterpriseCreate
            {
                Name = name,
                EmergencyPhone = emergencyPhone,
                Url = enterpriseUrl
            };

            return await this.PostAsync<EnterpriseInfoObjectTags>($"{this.EndpointUrl}/enterprise", data, cancellationToken);
        }

        /// <summary>
        /// Gets an enterprise details
        /// </summary>
        /// <param name="enterpriseId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<EnterpriseInfoObjectTags> GetEnterprise(
            string enterpriseId,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetEnterpriseAsync(enterpriseId, cancellationToken).Result;
        public async Task<WebCallResult<EnterpriseInfoObjectTags>> GetEnterpriseAsync(
            string enterpriseId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetAsync<EnterpriseInfoObjectTags>($"{this.EndpointUrl}/enterprise/{enterpriseId}", cancellationToken);
        }

        /// <summary>
        /// Updates number of required approvals  for an enterprise
        /// </summary>
        /// <param name="enterpriseId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="approvalsRequired"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<PendingApprovalInfo> UpdateEnterprise(
            string enterpriseId,
            int approvalsRequired,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.UpdateEnterpriseAsync(enterpriseId, approvalsRequired, cancellationToken).Result;
        public async Task<WebCallResult<PendingApprovalInfo>> UpdateEnterpriseAsync(
            string enterpriseId,
            int approvalsRequired,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new RequestData_EnterpriseUpdate
            {
                ApprovalsRequired = approvalsRequired,
            };

            return await this.PutAsync<PendingApprovalInfo>($"{this.EndpointUrl}/enterprise/{enterpriseId}", data, cancellationToken);
        }


        /// <summary>
        /// Lists enterprise users
        /// </summary>
        /// <param name="enterpriseId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="allowInactiveAdmins">Whether inactive admins should be returned as well</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<EnterpriseUsers> GetEnterpriseUsers(
            string enterpriseId,
            bool allowInactiveAdmins,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetEnterpriseUsersAsync(enterpriseId, allowInactiveAdmins, cancellationToken).Result;
        public async Task<WebCallResult<EnterpriseUsers>> GetEnterpriseUsersAsync(
            string enterpriseId,
            bool allowInactiveAdmins,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = this.ConvertToQueryString(new Dictionary<string, object>() {
                { "allowInactiveAdmins", allowInactiveAdmins },
            });

            return await this.GetAsync<EnterpriseUsers>($"{this.EndpointUrl}/enterprise/{enterpriseId}/user" + query, cancellationToken);
        }

        /// <summary>
        /// Adds User To Enterprise
        /// </summary>
        /// <param name="enterpriseId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="username">The Username of the User that should be added to the Enterprise</param>
        /// <param name="usernames"></param>
        /// <param name="permission">Value:"admin"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<PendingApprovalInfo> AddUserToEnterprise(
            string enterpriseId,
            string username,
            string[] usernames,
            string permission,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.AddUserToEnterpriseAsync(enterpriseId, username, usernames, permission, cancellationToken).Result;
        public async Task<WebCallResult<PendingApprovalInfo>> AddUserToEnterpriseAsync(
            string enterpriseId,
            string username,
            string[] usernames,
            string permission,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new RequestData_EnterpriseAddUser
            {
                Username = username,
                Usernames = usernames,
                Permission = permission,
            };

            return await this.PostAsync<PendingApprovalInfo>($"{this.EndpointUrl}/enterprise/{enterpriseId}/user", data, cancellationToken);
        }

        /// <summary>
        /// Removes User From Enterprise
        /// </summary>
        /// <param name="enterpriseId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="username">The Username of the User that should be removed from the Enterprise</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<PendingApprovalInfo> RemoveUserFromEnterprise(
            string enterpriseId,
            string username,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.RemoveUserFromEnterpriseAsync(enterpriseId, username, cancellationToken).Result;
        public async Task<WebCallResult<PendingApprovalInfo>> RemoveUserFromEnterpriseAsync(
            string enterpriseId,
            string username,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new RequestData_EnterpriseRemoveUser
            {
                Username = username,
            };

            return await this.DeleteAsync<PendingApprovalInfo>($"{this.EndpointUrl}/enterprise/{enterpriseId}/user", data, cancellationToken);
        }

        /// <summary>
        /// Freezes the enterprise for limited time
        /// </summary>
        /// <param name="enterpriseId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="durationInSeconds">seconds to freeze the enterprise for</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<Freeze> FreezeEnterprise(
            string enterpriseId,
            int durationInSeconds,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.FreezeEnterpriseAsync(enterpriseId, durationInSeconds, cancellationToken).Result;
        public async Task<WebCallResult<Freeze>> FreezeEnterpriseAsync(
            string enterpriseId,
            int durationInSeconds,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new RequestData_EnterpriseFreeze
            {
                Duration = durationInSeconds,
            };

            return await this.PostAsync<Freeze>($"{this.EndpointUrl}/enterprise/{enterpriseId}/freeze", data, cancellationToken);
        }

        /// <summary>
        /// Gets enterprise's wallet limits
        /// </summary>
        /// <param name="enterpriseId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="coin">Filter by coin(s). Example: ["btc"]</param>
        /// <param name="isCustodial">Whether custodial limits should be returned</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<EnterpriseWalletLimits> GetEnterpriseWalletLimits(
            string enterpriseId,
            string[] coin = null,
            bool? isCustodial = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetEnterpriseWalletLimitsAsync(enterpriseId, coin, isCustodial, cancellationToken).Result;
        public async Task<WebCallResult<EnterpriseWalletLimits>> GetEnterpriseWalletLimitsAsync(
            string enterpriseId,
            string[] coin = null,
            bool? isCustodial = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = this.ConvertToQueryString(new Dictionary<string, object>() {
                { "coin", "[\"" + string.Join("\",\"", coin) + "\"]" },
                { "isCustodial", isCustodial },
            });

            return await this.GetAsync<EnterpriseWalletLimits>($"{this.EndpointUrl}/enterprise/{enterpriseId}/walletLimits" + query, cancellationToken);
        }
        #endregion
    }
}
using BPNewMVCTest.Models;
using BPNewMVCTest1Service.HttpService;
using BPNewMVCTest1Service.TokenDTService;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace BPNewMVCTest1.Models
{
    public class BoardPACUserStore : IUserStore<IdentityUser>,
                         IUserClaimStore<IdentityUser>,
                         IUserLoginStore<IdentityUser>,
                         IUserRoleStore<IdentityUser>,
                         IUserPasswordStore<IdentityUser>,
                         IUserSecurityStampStore<IdentityUser>
    {
        //private ITokenDTService _tokenDTService;
        private IHttpService _httpService;
        public BoardPACUserStore(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public Task AddClaimsAsync(IdentityUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task AddLoginAsync(IdentityUser user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task AddToRoleAsync(IdentityUser user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> CreateAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public async Task<IdentityUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {

            HttpClient client = _httpService.GetHttpClientInstance();
            HttpResponseMessage response = await client.PostAsJsonAsync(_httpService.GetBaseURL() + "auth/findbyid", userId);

            IdentityUser identityUser = null;
            if (response.IsSuccessStatusCode)
            {
                identityUser = await response.Content.ReadAsAsync<IdentityUser>();
            }

            return identityUser;
            //throw new NotImplementedException();
        }

        public Task<IdentityUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            //var identityUser = new IdentityUser() {Email = normalizedUserName, UserName = normalizedUserName };

            //HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:44354/api/auth/findbyname", normalizedUserName);

            HttpClient client = _httpService.GetHttpClientInstance();
            HttpResponseMessage response = await client.PostAsJsonAsync(_httpService.GetBaseURL() + "auth/findbyname", normalizedUserName);

            IdentityUser identityUser = null;
            if (response.IsSuccessStatusCode)
            {
                identityUser = await response.Content.ReadAsAsync<IdentityUser>();
            }

            return identityUser;
            //throw new NotImplementedException();
        }

        public Task<IList<Claim>> GetClaimsAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            IList<Claim> claims = new List<Claim>();

            return Task.FromResult(claims);
            //throw new NotImplementedException();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            IList<UserLoginInfo> UserLoginInfoList = new List<UserLoginInfo>();
            return Task.FromResult(UserLoginInfoList);
            //throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetPasswordHashAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            //HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //var credentialViewModel = new CredentialsViewModel() { UserName = "bhathiyad@irononetech.com", Password = "Abc_123+" };

            //HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:44354/api/auth/loginexternal", credentialViewModel);

            //IdentityUser identityUser = null;
            //if (response.IsSuccessStatusCode)
            //{
            //    identityUser = await response.Content.ReadAsAsync<IdentityUser>();
            //}

            return Task.FromResult(user.PasswordHash);
        }

        public Task<IList<string>> GetRolesAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            IList<string> UserRoleList = new List<string>();
            return Task.FromResult(UserRoleList);
            //throw new NotImplementedException();
        }

        public Task<string> GetSecurityStampAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.SecurityStamp);
        }

        public Task<string> GetUserIdAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
            //throw new NotImplementedException();
        }

        public Task<string> GetUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
            //throw new NotImplementedException();
        }

        public Task<IList<IdentityUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            IList<IdentityUser> UserClaimList = new List<IdentityUser>();
            return Task.FromResult(UserClaimList);

            //throw new NotImplementedException();
        }

        public Task<IList<IdentityUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            IList<IdentityUser> UsersRoleList = new List<IdentityUser>();
            return Task.FromResult(UsersRoleList);
            //throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(IdentityUser user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimsAsync(IdentityUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(IdentityUser user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLoginAsync(IdentityUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task ReplaceClaimAsync(IdentityUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(IdentityUser user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(IdentityUser user, string passwordHash, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetSecurityStampAsync(IdentityUser user, string stamp, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(IdentityUser user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

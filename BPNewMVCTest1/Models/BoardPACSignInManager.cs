using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BPNewMVCTest.Models
{

    public class BoardPACSignInManager<TUser> : SignInManager<TUser> where TUser : class
    {
        public BoardPACSignInManager(UserManager<TUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<TUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<TUser>> logger, IAuthenticationSchemeProvider schemes)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes) { }

        public override async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return await base.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
        }
    }

    public class CredentialsViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

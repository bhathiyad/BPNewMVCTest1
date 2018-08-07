using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using BPNewMVCTest.Models;
using BPNewMVCTest1Service.TokenDTService;
using BPNewMVCTest1Service.HttpService;
using System.Net.Http;
using BPNewMVCTest1.Models;
using Newtonsoft.Json;

namespace BPNewMVCTest1.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        //private readonly BoardPACSignInManager<IdentityUser> _signInManager; 
        private readonly ILogger<LoginModel> _logger;
        private ITokenDTService _tokenDTService;
        private IHttpService _httpService;

        public LoginModel(SignInManager<IdentityUser> signInManager, ILogger<LoginModel> logger, 
                            ITokenDTService tokenDTService, IHttpService httpService) //BoardPACSignInManager<IdentityUser> signInManager
        {
            _signInManager = signInManager;
            _logger = logger;
            _httpService = httpService;
            _tokenDTService = tokenDTService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    var credViewModel = new Models.CredentialsViewModel() { UserName = Input.Email, Password = Input.Password };
                    HttpClient client = _httpService.GetHttpClientInstance();
                    HttpResponseMessage response = await client.PostAsJsonAsync(_httpService.GetBaseURL() + "auth/login", credViewModel);

                    if (response.IsSuccessStatusCode)
                    {
                        var tokenDetails = await response.Content.ReadAsAsync<string>();
                        var tokenObj = JsonConvert.DeserializeObject<AuthTokenModel>(tokenDetails);
                        _tokenDTService.SetToken(tokenObj.Auth_Token);
                    }

                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}

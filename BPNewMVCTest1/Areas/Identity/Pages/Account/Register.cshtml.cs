using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BPNewMVCTest1.Models;
using BPNewMVCTest1Service.HttpService;
using BPNewMVCTest1Service.TokenDTService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BPNewMVCTest1.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private IHttpService _httpService;
        private ITokenDTService _tokenDTService;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IHttpService httpService,
             ITokenDTService tokenDTService) //IEmailSender emailSender
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            //_emailSender = emailSender;
            _httpService = httpService;
            _tokenDTService = tokenDTService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                //var result = await _userManager.CreateAsync(user, Input.Password);
                dynamic userWithPassword = new System.Dynamic.ExpandoObject();
                userWithPassword.user = user;
                userWithPassword.password = Input.Password;

                HttpClient client = _httpService.GetHttpClientInstance();
                HttpResponseMessage response = await client.PostAsJsonAsync(_httpService.GetBaseURL() + "auth/CreateAsync", new { user, Input.Password });

                bool result = false;
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<bool>();
                }

                if (result)//result.Succeeded
                {
                    _logger.LogInformation("User created a new account with password.");

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { userId = user.Id, code = code },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    var res = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, false, lockoutOnFailure: true);

                    if (res.Succeeded)
                    {
                        var credViewModel = new Models.CredentialsViewModel() { UserName = Input.Email, Password = Input.Password };
                        HttpClient client1 = _httpService.GetHttpClientInstance();
                        HttpResponseMessage response1 = await client.PostAsJsonAsync(_httpService.GetBaseURL() + "auth/login", credViewModel);

                        if (response.IsSuccessStatusCode)
                        {
                            var tokenDetails = await response1.Content.ReadAsAsync<string>();
                            var tokenObj = JsonConvert.DeserializeObject<AuthTokenModel>(tokenDetails);
                            _tokenDTService.SetToken(tokenObj.Auth_Token);
                        }

                        _logger.LogInformation("User logged in.");
                        return LocalRedirect(returnUrl);
                    }

                    return LocalRedirect(returnUrl);
                }
                //foreach (var error in result.Errors)
                //{
                //    ModelState.AddModelError(string.Empty, error.Description);
                //}
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}

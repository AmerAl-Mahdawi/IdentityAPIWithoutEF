using IdentityAPIWithoutEF.Library.Models;
using IdentityAPIWithoutEF.Library.Stores;
using IdentityAPIWithoutEF.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using IdentityAPIWithoutEF.Extensions;
using IdentityAPIWithoutEF.Services;
using System;

namespace IdentityAPIWithoutEF.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserStore _userStore;
        private readonly UserManager<UserModel> _userManager;
        private readonly RoleManager<RoleModel> _roleManager;
        private readonly ILogger<UserModel> _logger;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly IEmailSender _emailSender;

        public UserController(IUserStore userStore,
                              UserManager<UserModel> userManager,
                              RoleManager<RoleModel> roleManager,
                              ILogger<UserModel> logger,
                              SignInManager<UserModel> signInManager,
                              IEmailSender emailSender)
        {
            _userStore = userStore;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task Login()
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            _logger.LogInformation("Existing external cookie cleared out.");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task Login(LoginApiModel loginApimodel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginApimodel.Email, loginApimodel.Password, loginApimodel.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                }
                else
                {
                    _logger.LogWarning("Invalid login attempt.");
                }
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task Register(RegisterApiModel registerApimodel)
        {
            if (ModelState.IsValid)
            {
                var user = new UserModel
                {
                    UserName = registerApimodel.Email,
                    Email = registerApimodel.Email,
                    PhoneNumber = registerApimodel.PhoneNumber
                };

                var result = await _userManager.CreateAsync(user, registerApimodel.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    await _emailSender.SendEmailConfirmationAsync(registerApimodel.Email, callbackUrl);

                    _logger.LogInformation("Email address confirmation has been sent.");
                }
                else
                {
                    _logger.LogInformation("User not created.");
                }
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                _logger.LogInformation("UserId or Code are null.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            _logger.LogInformation("User email has been confirmed.");
        }

        [HttpPost]
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
        }

        [HttpPost]
        [Authorize]
        public async Task DeleteAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User Deleted Successfully.");
                }
                else
                {
                    _logger.LogInformation("User Not Deleted.");
                }
            }
        }

        [HttpPost]
        [Authorize]
        public async Task AssignRoleToUserAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                var hasRole = await _userManager.IsInRoleAsync(user, roleName);
                if (hasRole == false)
                {
                    var result = await _userManager.AddToRoleAsync(user, roleName);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("Role Added to User Successfully.");
                    }
                    else
                    {
                        _logger.LogInformation("Role Not Added to User.");
                    }
                }
                else
                {
                    _logger.LogInformation($"User already has Role { roleName }.");
                }
            }
            else
            {
                _logger.LogInformation($"Role { roleName } not exists in the system.");
            }
        }
    }
}

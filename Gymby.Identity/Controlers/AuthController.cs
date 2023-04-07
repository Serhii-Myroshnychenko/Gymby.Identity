using Gymby.Identity.Models;
using Gymby.Identity.ViewModels;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gymby.Identity.Controlers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IIdentityServerInteractionService _interaction;
        public AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IIdentityServerInteractionService interaction)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _interaction = interaction;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var viewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await _userManager.FindByNameAsync(viewModel.Username);

            if(user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found");
                return View(viewModel);
            }

            var result = await _signInManager.PasswordSignInAsync(viewModel.Username,
                viewModel.Password, false, false);

            if (result.Succeeded)
            {
                return Redirect(viewModel.ReturnUrl);
            }

            ModelState.AddModelError(string.Empty, "Login error");
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            var viewModel = new RegisterViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(viewModel);

        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = new AppUser
            {
                UserName = viewModel.Username,
                Email = viewModel.Email
            };
            var result = await _userManager.CreateAsync(user, viewModel.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Redirect(viewModel.ReturnUrl);
            }

            ModelState.AddModelError(string.Empty, "Error occurred");
            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var logoutRequest = await _interaction.GetLogoutContextAsync(logoutId);
            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }
    }
}

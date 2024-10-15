using ExamForScoolchildrenApp.Domain.Entities;
using ExamForScoolchildrenApp.Domain.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using ExamForScoolchildrenApp.Aplication.Services;

namespace ExamForScoolchildrenApp.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly UserService _userService;
        private readonly SendEmailService _sendEmailService;

        public AccountController(
                             UserManager<AppUser> userManager,
                            SignInManager<AppUser> signInManager,
                            UserService userService,
                            SendEmailService sendEmailService
                          )
        {
            _signInManager = signInManager;
            _userManager = userManager;
           _userService = userService;
            _sendEmailService = sendEmailService;  
        }

        [HttpGet("create")]
        public IActionResult RegisterUser()
        {
            UserRegisterModel userRegisterModel = new UserRegisterModel();

            return View(userRegisterModel);
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(UserRegisterModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            await _userService.AddUserAsync(registerViewModel);

            await _sendEmailService.SendEmail();

            return View("VerifyEmail");
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var codeDecodedBytes = WebEncoders.Base64UrlDecode(token);
                var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);

                IdentityResult result = await _userManager.ConfirmEmailAsync(user, codeDecoded);

                if (result.Succeeded)
                {
                    return View();
                }
            }

            return View("FailedConfirmation");
        }


        [HttpGet("Signin")]
        public IActionResult Signin()
        {
            return View();
        }


        [HttpPost("Signin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signin(UserLoginModel userLoginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userLoginModel);
            }

            AppUser user = await _userManager.FindByEmailAsync(userLoginModel.EmailOrUsername);

            if (user == null)
            {
                user = await _userManager.FindByNameAsync(userLoginModel.EmailOrUsername);
            }

            if (user == null)
            {
                ModelState.AddModelError("", "Email or password is invalid");
                return View(userLoginModel);
            }

            if (!user.EmailConfirmed)
            {
                ModelState.AddModelError("", "Your email is not confirmed yet. Please, check your email.");
                return View(userLoginModel);
            }

            var result = await _signInManager.PasswordSignInAsync(user, userLoginModel.Password, userLoginModel.RememberMe, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Email or password is invalid");
            return View(userLoginModel);
        }

        public IActionResult Signout()
        {
            _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

    }
}

using ExamForScoolchildrenApp.Aplication.Interface;
using ExamForScoolchildrenApp.Domain.Entities;
using ExamForScoolchildrenApp.Domain.ViewModels;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ExamForScoolchildrenApp.Aplication.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;

namespace ExamForScoolchildrenApp.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(
                            IEmailSender emailSender,
                             UserManager<AppUser> userManager,
                            SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _emailSender = emailSender;
            _userManager = userManager;
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

            var user = new AppUser
            {

                Name = registerViewModel.Name,
                Surname = registerViewModel.Surname,
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email,
                PhoneNumber = registerViewModel.PhoneNumber,
                Address = registerViewModel.Address,
                Password = registerViewModel.Password
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerViewModel.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(registerViewModel);
            }

            //email confirmation
            //SMTP
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            //64 byte array conversion part
            byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
            var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);

            await _emailSender.SendEmailAsync(registerViewModel.Email, "Conform your email",

                 $"Confirm your account by following to " +
                $"<a href='{HtmlEncoder.Default.Encode($"https://localhost:7032/Account/ConfirmEmail?token={codeEncoded}&userId={user.Id}")}'>" +
                "this link" +
                $"</a>");


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

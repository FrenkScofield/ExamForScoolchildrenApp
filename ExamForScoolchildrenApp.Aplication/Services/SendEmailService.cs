using ExamForScoolchildrenApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using Newtonsoft.Json;

namespace ExamForScoolchildrenApp.Aplication.Services
{
    public class SendEmailService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SendEmailService(UserManager<AppUser> userManager, 
                                IEmailSender emailSender,
                                IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SendEmail()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var currentUserSession = session.GetString("UserInfoObject");

            // Deserialize the user object
            var user = JsonConvert.DeserializeObject<AppUser>(currentUserSession);

            //email confirmation
            //SMTP
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            //64 byte array conversion part
            byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
            var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);

            await _emailSender.SendEmailAsync(user.Email, "Conform your email",

                 $"Confirm your account please to click" +
                $"<a href='{HtmlEncoder.Default.Encode($"https://localhost:7032/Account/ConfirmEmail?token={codeEncoded}&userId={user.Id}")}'>" +
                "    CONFIRM" +
                $"</a>");
            //style ="margin: 0px 22px; background-color: #3170df; font-size: 20px; border: 1px solid #31b5e5; padding: 4px 16px; color: WHITE; border-radius: 15px; font-weight: 600; box-shadow: 4px 17px 10px 5px silver;"
        }
    }
}

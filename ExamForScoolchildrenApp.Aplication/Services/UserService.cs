using ExamForScoolchildrenApp.Domain.Entities;
using ExamForScoolchildrenApp.Domain.ViewModels;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace ExamForScoolchildrenApp.Aplication.Services
{

    public class UserService 
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AddUserAsync(UserRegisterModel registerViewModel)
        {
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
          await _userManager.CreateAsync(user, registerViewModel.Password);

            // Serialize the user object and store it in session
            var session = _httpContextAccessor.HttpContext.Session;
            session.SetString("UserInfoObject", JsonConvert.SerializeObject(user));
        }
    }
}

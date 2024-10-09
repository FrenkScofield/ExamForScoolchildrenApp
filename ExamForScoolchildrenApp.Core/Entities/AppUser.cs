using Microsoft.AspNetCore.Identity;

namespace ExamForScoolchildrenApp.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }
    }
}

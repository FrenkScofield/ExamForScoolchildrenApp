using System.ComponentModel.DataAnnotations;

namespace ExamForScoolchildrenApp.Domain.ViewModels
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Email or Username can not be null"), StringLength(50)]
        public string EmailOrUsername { get; set; }

        [Required(ErrorMessage = "Password can not be null"), DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}

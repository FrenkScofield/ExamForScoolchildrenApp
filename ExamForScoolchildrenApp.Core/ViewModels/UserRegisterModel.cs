using System.ComponentModel.DataAnnotations;

namespace ExamForScoolchildrenApp.Domain.ViewModels
{
    public class UserRegisterModel
    {
        [Required(ErrorMessage = "Name can not be empty"), StringLength(50)]
        public string Name { get; set; }

        [ StringLength(50)]
        public string Surname { get; set; }

        [ StringLength(50)]
        public string UserName { get; set; }

        [ StringLength(50)]
        public string Email { get; set; }

        [ StringLength(150)]
        public string Address { get; set; }

        [ StringLength(150)]
        public string PhoneNumber { get; set; }

        [Required, StringLength(50)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password doesn't match each other"), StringLength(100)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}

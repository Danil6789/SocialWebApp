using System.ComponentModel.DataAnnotations;

namespace SocialWebApp.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "Адрес эл. почта")]
        [Required(ErrorMessage = "Требуется почта")]
        public string? Email { get; set; }


        [Required(ErrorMessage = "Требуется пароль")]
        [Display(Name = "Пароль")]

        public string? Password { get; set; }


        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }
    }
}

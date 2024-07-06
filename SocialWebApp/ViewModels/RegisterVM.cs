using System.ComponentModel.DataAnnotations;

namespace SocialWebApp.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Требуется имя пользователя")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Требуется почта")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Требуется пароль")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароль не совпадает")]
        [Required(ErrorMessage = "Требуется подтверждение пароля")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace dotnetAuthentication.Models.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "email zorunlu!")]
        [EmailAddress(ErrorMessage = "email formatında bir giriş yapın!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "şifre zorunlu!")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
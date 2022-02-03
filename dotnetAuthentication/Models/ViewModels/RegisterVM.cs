using System.ComponentModel.DataAnnotations;

namespace dotnetAuthentication.Models.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "email zorunlu!")]
        [EmailAddress(ErrorMessage = "email formatında bir giriş yapın!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "şifre zorunlu!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "şifre (tekrar) zorunlu!")]
        [Compare("Password", ErrorMessage = "şifreler uymuyor!")]
        public string ConfirmPassword { get; set; }
    }
}
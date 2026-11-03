using System.ComponentModel.DataAnnotations;

namespace FaceKid.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Ad soyad zorunludur")]
        public string FullName {  get; set; } = string.Empty;

        [Required(ErrorMessage = "E posta zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir e posta adresi giriniz!")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre zorunludur")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}

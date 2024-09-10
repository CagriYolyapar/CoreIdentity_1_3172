using System.ComponentModel.DataAnnotations;

namespace CoreIdentity_1.Models.ViewModels.AppUsers.PureVms.RequestModels
{
    //Dikkat edin burada CreateUserRequestModel demedik...
    public class UserRegisterRequestModel
    {
        [Required(ErrorMessage ="{0} alanı zorunludur")]
        [Display(Name ="Kullanıcı ismi")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Parola alanı zorunludur")]
        [MinLength(3,ErrorMessage ="Minimum 3 karakter girilmesi gereklidir")]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage ="Parolalar uyusmuyor")]
        public string ConfirmPassword { get; set; }

        [EmailAddress(ErrorMessage ="Email adres formatına uygun bir giriş yapınız")]
        public string Email { get; set; }

    }
}

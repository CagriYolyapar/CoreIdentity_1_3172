using System.ComponentModel.DataAnnotations;

namespace CoreIdentity_1.Models.ViewModels.AppUsers.PureVms.RequestModels
{

    //.Net 7'den sonra referans tiplerine bos gecilebilir ifadesi koymazsanız (?) orası required olarak algılanır...
    public class UserSignInRequestModel
    {
        [Required(ErrorMessage ="{0} zorunludur")]
        [Display(Name ="Kullanıcı ismi")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Sifre alanı zorunludur")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }

    }
}

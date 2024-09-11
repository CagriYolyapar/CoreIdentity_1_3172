using System.ComponentModel.DataAnnotations;

namespace CoreIdentity_1.Models.Administrator.ViewModels.AppUsers.PureVms.RequestModels
{
    public class CreateUserRequestModel
    {
        [Required(ErrorMessage ="Kullanıcı ismi alanı girilmesi zorunludur")]
        public string UserName { get; set; }

        [EmailAddress(ErrorMessage ="Email formatında giriş yapılması zorunludur")]
        public string Email { get; set; }

    }
}

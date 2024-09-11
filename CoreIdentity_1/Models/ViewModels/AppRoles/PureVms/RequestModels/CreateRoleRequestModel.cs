using System.ComponentModel.DataAnnotations;

namespace CoreIdentity_1.Models.ViewModels.AppRoles.PureVms.RequestModels
{
    public class CreateRoleRequestModel
    {
        [Required(ErrorMessage = "Rol ismi gereklidir")]
        public string RoleName { get; set; }

    }
}

using CoreIdentity_1.Models.Administrator.ViewModels.AppRoles.PureVms.ResponseModels;

namespace CoreIdentity_1.Models.Administrator.ViewModels.AppRoles.PageVms
{
    public class AssignRolePageVM
    {
        public List<AppRoleResponseModel> Roles { get; set; }
        public int UserID { get; set; }
    }
}

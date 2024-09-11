using CoreIdentity_1.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreIdentity_1.Controllers
{
    [Authorize(Roles ="Admin")]
    [AutoValidateAntiforgeryToken]
    public class UserController : Controller
    {
        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<AppRole> _roleManager;

        public UserController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            List<AppUser> allUsers = _userManager.Users.ToList();

            //List<AppUser> nonAdminUsers  = _userManager.Users.Where(x => !x.UserRoles.Any(x => x.Role.Name == "Admin")).ToList();
            return View(allUsers);
        }
    }
}

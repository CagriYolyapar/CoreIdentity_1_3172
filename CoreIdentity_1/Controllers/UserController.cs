using CoreIdentity_1.Models.Administrator.ViewModels.AppRoles.PageVms;
using CoreIdentity_1.Models.Administrator.ViewModels.AppRoles.PureVms.ResponseModels;
using CoreIdentity_1.Models.Administrator.ViewModels.AppUsers.PureVms.RequestModels;
using CoreIdentity_1.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRequestModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new()
                {
                    UserName = model.UserName,
                    Email = model.Email
                };

                IdentityResult identityResult = await _userManager.CreateAsync(appUser, $"{model.UserName}cgr123");

                if (identityResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(appUser, "Member");
                    return RedirectToAction("Index");
                }

                foreach (IdentityError error in identityResult.Errors)
                {

                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> AssignRole(int id)
        {
            AppUser appUser = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == id);

            IList<string> userRoles = await _userManager.GetRolesAsync(appUser); //Elimize gecen kullanıcının rolleri
            List<AppRole> allRoles = _roleManager.Roles.ToList(); //bütün roller

            List<AppRoleResponseModel> responseRoles = new(); //bu listenin amacı kesisimleri tutarak kimin checkli gelecegini belirlemek

            foreach (AppRole item in allRoles)
            {
                responseRoles.Add(new()
                {
                    RoleID = item.ID,
                    RoleName = item.Name,
                    Checked = userRoles.Contains(item.Name)
                });
            }

            AssignRolePageVM arVm = new()
            {
                UserID = id,
                Roles = responseRoles
            };

            return View(arVm);

        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(AssignRolePageVM model)
        {
            AppUser appUser = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == model.UserID);

            IList<string> userRoles = await _userManager.GetRolesAsync(appUser);

            foreach (AppRoleResponseModel role in model.Roles)
            {
                if (role.Checked && !userRoles.Contains(role.RoleName)) await _userManager.AddToRoleAsync(appUser, role.RoleName);
                else if(!role.Checked && userRoles.Contains(role.RoleName)) await _userManager.RemoveFromRoleAsync(appUser,role.RoleName);
            }

            return RedirectToAction("Index");
        }

    }
}

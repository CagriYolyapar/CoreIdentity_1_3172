using CoreIdentity_1.Models.Entities;
using CoreIdentity_1.Models.ViewModels.AppRoles.PureVms.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreIdentity_1.Controllers
{
    [Authorize(Roles ="Admin")]
    [AutoValidateAntiforgeryToken]
    public class RoleController : Controller
    {
        readonly RoleManager<AppRole> _roleManager;
        

        public RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
        
        }

        public IActionResult Index()
        {
            return View(_roleManager.Roles.ToList());
        }

        public IActionResult Create()
        {
            return View();
            
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleRequestModel model)
        {
            if (ModelState.IsValid) 
            {
               IdentityResult identityResulty =  await _roleManager.CreateAsync(new() { Name = model.RoleName });

                if (identityResulty.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (IdentityError error in identityResulty.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }


            return View(model);
        }
    }
}

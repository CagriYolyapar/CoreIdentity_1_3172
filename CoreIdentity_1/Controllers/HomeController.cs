using CoreIdentity_1.Models;
using CoreIdentity_1.Models.ContextClasses;
using CoreIdentity_1.Models.Entities;
using CoreIdentity_1.Models.ViewModels.AppUsers.PureVms.RequestModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CoreIdentity_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //Identity kütüpahnesi crud ve servis işlemleri icin bir takım class'lara sahiptir...Bu Manager Class'ları sizin ilgili Identity yapılarınızın Crud işlemlerine ve baska business logic girmesini saglarlar...


        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<AppRole> _roleManager;
        readonly SignInManager<AppUser> _signInManager;

        //Manager'lar bu sekilde arttıgında dogal olarak constructor'da parametreler de arttıgından dolayı Constructor hell durumu ile karsılasabiliriz...Mediat-r.. Constructor Hell...




        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;



        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new()
                {
                    UserName = model.UserName,
                    Email = model.Email


                };

                IdentityResult identityResult = await _userManager.CreateAsync(appUser, model.Password);
                if (identityResult.Succeeded)
                {
                    AppRole appRole = await _roleManager.FindByNameAsync("Admin"); //Admin ismindeki rolü bulabilirse Role nesnesini AppRole'e atacak bulamazsa appRole null olacak
                    if (appRole == null) await _roleManager.CreateAsync(new() { Name="Admin"}); //Admin isminde bir role yarattık
                    await _userManager.AddToRoleAsync(appUser, "Admin"); //appUser degişkeninin tuttugu kullanıcı nesnesine Admin isimli rolü ekledik...

                    return RedirectToAction("Index");
                }

                foreach (IdentityError error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                

            }

            return View(model);
        }
    }
}

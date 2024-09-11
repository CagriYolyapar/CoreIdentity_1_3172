using CoreIdentity_1.Models;
using CoreIdentity_1.Models.ContextClasses;
using CoreIdentity_1.Models.Entities;
using CoreIdentity_1.Models.ViewModels.AppUsers.PureVms.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
//using Digeri = Microsoft.AspNetCore.Mvc.SignInResult;

namespace CoreIdentity_1.Controllers
{
    [AutoValidateAntiforgeryToken] //Get ile gelen sayfada verilen özel bir token sayesinde Post isteklerinin bu token'siz yapılamamasını saglar...PostMan gibi third part software'lerden gözlemlediginizde direkt Post tarafına ulasamayacagınızı göreceksiniz...
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
                    #region AdminEklemekIcinTekKullanimlikKod
                    //AppRole appRole = await _roleManager.FindByNameAsync("Admin"); //Admin ismindeki rolü bulabilirse Role nesnesini AppRole'e atacak bulamazsa appRole null olacak
                    //if (appRole == null) await _roleManager.CreateAsync(new() { Name = "Admin" }); //Admin isminde bir role yarattık
                    //await _userManager.AddToRoleAsync(appUser, "Admin"); //appUser degişkeninin tuttugu kullanıcı nesnesine Admin isimli rolü ekledik... 
                    #endregion

                    AppRole appRole = await _roleManager.FindByNameAsync("Member");
                    if (appRole == null) await _roleManager.CreateAsync(new() { Name = "Member" });
                    await _userManager.AddToRoleAsync(appUser, "Member"); //Register artık bu kod sayesinde direkt kayıt olan kişiye Member rolü verecek

                    return RedirectToAction("Index");
                }

                foreach (IdentityError error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }


            }

            return View(model);
        }

        //Unutmayın ki Identity User nesnesine ulasabilmek icin Authorize attribute'unuzun olması gereklidir



        [Authorize(Roles = "Admin")]
        public IActionResult AdminPanel()
        {

            return View();
        }

        [Authorize(Roles = "Member")]
        public IActionResult MemberPanel()
        {
            return View();
        }


        public IActionResult Panel()
        {
            return View();
        }

        public IActionResult SignIn(string returnUrl)
        {
            UserSignInRequestModel usModel = new()
            {
                ReturnUrl = returnUrl,
            };
            return View(usModel);
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInRequestModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = await _userManager.FindByNameAsync(model.UserName); //await ile bir Task'in direkt sonucunu beklediginiz icin onu ele alırsınız

                if (appUser == null)
                {
                    TempData["Message"] = "Kullanıcı bulunamadı";
                    return RedirectToAction("SignIn");
                }

                SignInResult signInResult = await _signInManager.PasswordSignInAsync(appUser, model.Password, model.RememberMe, true);




                

                if (signInResult.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    IList<string> roles = await _userManager.GetRolesAsync(appUser);
                    if (roles.Contains("Admin")) return RedirectToAction("AdminPanel");
                    else if (roles.Contains("Member")) return RedirectToAction("MemberPanel");
                    return RedirectToAction("Panel");

                }
                else if (signInResult.IsLockedOut)
                {
                    DateTimeOffset? lockOutEndDate = await _userManager.GetLockoutEndDateAsync(appUser);
                    ModelState.AddModelError("", $"Hesabınız {(lockOutEndDate.Value.UtcDateTime - DateTime.UtcNow).Minutes} dakika süreyle askıya alınmıstır");
                }

                else
                {
                    string message = "";



                    int maxFailedAttempts = _userManager.Options.Lockout.MaxFailedAccessAttempts; //bu,middleware'deki maksimum hata sayınız...
                    message = $"Eger {maxFailedAttempts - await _userManager.GetAccessFailedCountAsync(appUser)} kez daha yanlıs giriş yaparsanız hesabınız gecici olarak askıya alınacaktır";//buradaki  userManager'daki ise su ana kadar kac kez yanlıslık yaptınız..



                    ModelState.AddModelError("", message);
                }

            }

            return View(model);
        }


        public IActionResult AccessDenied()
        {
            return View();
        }


        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}

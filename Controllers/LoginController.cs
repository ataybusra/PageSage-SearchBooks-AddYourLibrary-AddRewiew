using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PageSage.Areas.Identity.Data;
using PageSage.Dto;

namespace PageSage.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager; // Giriş yöneticisi, kullanıcı oturum açma işlemlerini yönetir.
        private readonly UserManager<AppUser> _userManager; // Kullanıcı yöneticisi, kullanıcı hesaplarını yönetir.

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet] 
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost] 
        public async Task<IActionResult> Index(AppUserLoginDto appUserLoginDto)
        {
            var result = await _signInManager.PasswordSignInAsync(appUserLoginDto.Username, appUserLoginDto.Password, false, true); // Kullanıcı oturum açmaya çalışır.
            if (result.Succeeded) // Oturum açma başarılı ise
            {
                var user = await _userManager.FindByNameAsync(appUserLoginDto.Username); // Kullanıcı adına göre kullanıcı bulunur
                if (user != null)
                {
                    return RedirectToAction("SearchBooks", "GoogleBookService");
                }
            }
            else 
            {
                ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya şifre.");
            }
            return View(); // Giriş sayfasını tekrar görüntüler.
        }
    }

}


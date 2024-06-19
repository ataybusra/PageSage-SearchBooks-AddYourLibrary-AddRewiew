using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PageSage.Areas.Identity.Data;
using PageSage.Dto;

namespace PageSage.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager; // Kullanıcı yöneticisi, kullanıcı hesaplarını yönetir.

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(AppUserRegisterDto appUserRegisterDto)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser()
                {
                    UserName = appUserRegisterDto.Username,
                    Name = appUserRegisterDto.Name,
                    Surname = appUserRegisterDto.Surname,
                    Email = appUserRegisterDto.Email
                };

                // Kullanıcı kaydı
                var result = await _userManager.CreateAsync(appUser, appUserRegisterDto.Password);
                if (result.Succeeded) // Kayıt başarılı ise
                {
                    var user = await _userManager.FindByNameAsync(appUser.UserName); // Kullanıcı bulunur
                    user.EmailConfirmed = true; // E-posta doğrulamasını yap
                    await _userManager.UpdateAsync(user); // Kullanıcıyı güncelle
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View();
        }
    }
}

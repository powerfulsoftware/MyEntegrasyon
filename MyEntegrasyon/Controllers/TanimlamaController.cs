using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyEntegrasyon.Data.Entities;

namespace MyEntegrasyon.Controllers
{
    public class TanimlamaController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public TanimlamaController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager=userManager;
            _roleManager=roleManager;
        }

        public IActionResult Users()
        {
            List<AppUser> liste = _userManager.Users.ToList();
            return View(liste);
        }
        public IActionResult Roller()
        {
            return View();
        }
        public IActionResult RolEkle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RolEkle(UserRole role)
        {
            return View();
        }
        public IActionResult RolGuncelle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RolGuncelle(UserRole role)
        {
            return View();
        }
    }
}

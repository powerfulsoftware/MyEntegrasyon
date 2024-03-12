using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEntegrasyon.BusinessLayer.Results;
using MyEntegrasyon.Data;
using MyEntegrasyon.Data.Entities;
using MyEntegrasyon.Models.Messages;
using MyEntegrasyon.Models.ViewModel;
using Serilog.Data;

namespace MyEntegrasyon.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TanimlamaController : Controller
    {
        private readonly ILogger<TanimlamaController> _logger;
        private readonly MyContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public TanimlamaController(ILogger<TanimlamaController> logger, MyContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RolEkle(RolEkleViewModel roleVM)
        {
            if (!ModelState.IsValid)
            {
                return View(roleVM);
            }
            IdentityResult roleResult;
            bool isRoleExists = await _roleManager.RoleExistsAsync(roleVM.RolName!);
            if (!isRoleExists)
            {
                AppRole rol = new AppRole();
                rol.Name = roleVM.RolName!;
                rol.Description = roleVM.Description;

                _logger.LogInformation("Yönetici tarafından '" + roleVM.RolName + "' rolü eklendi");
                roleResult = await _roleManager.CreateAsync(rol);
            }
            return RedirectToAction(nameof(TanimlamaController.Roller), "Tanimlama");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult BuRoleAtanan(string Id)
        {
            List<UserRolesViewModel> viewModel = new List<UserRolesViewModel>();
            if (!string.IsNullOrWhiteSpace(Id))
            {
                var rol = _roleManager.Roles.Where(x => x.Id == int.Parse(Id)).FirstOrDefault();
                if (rol != null)
                {
                    var BuRoldekiler = _context.UserRoles.Where(x => x.RoleId == rol.Id).ToList();
                    foreach (var item in BuRoldekiler)
                    {
                        viewModel.Add(new UserRolesViewModel()
                        {
                            Id = _context.Users.Where(y => y.Id == item.UserId).FirstOrDefault()!.Id,
                            UserName = _context.Users.Where(y => y.Id == item.UserId).FirstOrDefault()!.Adi + " " + _context.Users.Where(y => y.Id == item.UserId).FirstOrDefault()!.Soyadi,
                            UserId = item.UserId,
                            RolName = rol.Name
                        });
                    }
                }
            }
            return View(viewModel);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RolAta(string id)
        {
            var viewModel = new UserRolAtamaViewModel();
            if (!string.IsNullOrWhiteSpace(id))
            {

                var user = await _userManager.FindByIdAsync(id);
                var userRoles = await _userManager.GetRolesAsync(user!);

                ////////////////////////

                viewModel.Email = user?.Email;
                viewModel.UserName = user?.Adi + " " + user?.Soyadi;

                var allRoles = await _roleManager.Roles.ToListAsync();
                viewModel.Roles = allRoles.Select(x => new RoleViewModel()
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    Selected = userRoles.Contains(x.Name!)
                }).ToArray();


            }

            return View(viewModel);


        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RolAta(UserRolAtamaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(viewModel.Id!);
                var userRoles = await _userManager.GetRolesAsync(user!);

                await _userManager.RemoveFromRolesAsync(user!, userRoles);
                await _userManager.AddToRolesAsync(user!, viewModel.Roles!.Where(x => x.Selected).Select(x => x.Name)!);

                ////////////////////////////////////////////////////////

                return RedirectToAction(nameof(TanimlamaController.Users), "Tanimlama");
            }

            return View(viewModel);
        }



        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> RoldenCikar(string Id, int UserId)
        {
            if (!string.IsNullOrWhiteSpace(Id))
            {
                var rol = _roleManager.Roles.Where(x => x.Id == int.Parse(Id)).FirstOrDefault();
                if (rol != null)
                {
                    var user = await _userManager.FindByIdAsync(UserId.ToString());
                    // var userRoles = await _userManager.GetRolesAsync(user!);

                    List<string> userRole = new List<string>();
                    userRole.Add(rol.Name!);


                    await _userManager.RemoveFromRolesAsync(user!, userRole);
                }
            }

            return RedirectToAction(nameof(TanimlamaController.Roller), "Tanimlama");

        }
    }
}

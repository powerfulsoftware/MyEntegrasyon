using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyEntegrasyon.BusinessLayer.Results;
using MyEntegrasyon.Data;
using MyEntegrasyon.Data.Entities;
using MyEntegrasyon.Models;
using MyEntegrasyon.Models.Messages;
using MyEntegrasyon.Models.Nebim;
using MyEntegrasyon.Models.ViewModel;
using MyEntegrasyon.Services;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;

namespace MyEntegrasyon.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyContext _context;
        private readonly ILogger<HomeController> _logger;
		private readonly IMathService _mathService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;


        private string Connect_Url = "http://95.70.226.23:1515/(S(fjcangjis432kyhkvtblqxia))/IntegratorService/Connect";
        public HomeController(MyContext myContext, ILogger<HomeController> logger, IMathService mathService,  UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _context = myContext;
            _logger = logger;
			_mathService = mathService;
			_userManager = userManager;
			_signInManager = signInManager;
        }

        public IActionResult Index()
        {

			//try
			//{
			//	decimal result = _mathService.Divide(5 , 0);
			//}
			//catch (Exception ex)
			//{
			//	_logger.LogWarning(ex + " An exception occured while dividing two numbers");
			//}

            return View();
        }



		[HttpGet]
		public IActionResult Login()
		{

			return View();
		}
		[HttpPost]
		//[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(ViewModelLogin userModel)
		{
			if (!ModelState.IsValid)
			{
				return View(userModel);
			}
				BusinessLayerResult<AppUser> res = new BusinessLayerResult<AppUser>();
				res.Result = await _userManager.FindByEmailAsync(userModel.Email);

				if (res.Result != null)
				{
					if (!res.Result.IsActive)
					{
						res.AddError(ErrorMessageCode.KullaniciAtifDegil, "Kullanýcý aktifleþtirilmemiþtir.");
						// res.AddError(ErrorMessageCode.CheckYourEmail, "Lütfen e-posta adresinizi kontrol ediniz.");
					}
				}
				else
				{
					res.AddError(ErrorMessageCode.KullaniciAdiVeyaSifreHatali, "Kullanýcý adý yada þifre uyuþmuyor.");
				}

				//////////////////////////////////////////////////////
				if (res.Errors.Count > 0)
				{
					res.Errors.ForEach(x => ModelState.AddModelError("", x.Message!));
					return View(userModel);
				}


				var user = await _userManager.FindByEmailAsync(userModel.Email);

				//if (user != null && await _userManager.CheckPasswordAsync(user, userModel.Password))
				if (user != null)
				{
					//var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
					//identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
					// identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
					//await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(identity));

					Microsoft.AspNetCore.Identity.SignInResult ress = _signInManager.PasswordSignInAsync(user.UserName!, userModel.Password!, false, lockoutOnFailure: false).Result;
					if (ress.Succeeded)
					{
						CookieOptions option = new CookieOptions();
						option.Expires = DateTimeOffset.Now.AddHours(3);
						option.MaxAge = TimeSpan.FromHours(5);
						// option.Path = "/Home/Login";

						Response.Cookies.Append("UserId", user.Id.ToString(), option);
						Response.Cookies.Append("AdiSoyadi", user.Adi + " " + user.Soyadi, option);


						return RedirectToAction(nameof(HomeController.Index), "Home");
					}
					else
					{
						res.AddError(ErrorMessageCode.KullaniciAdiVeyaSifreHatali, "Kullanýcý adý yada þifre uyuþmuyor.");
					}


					if (res.Errors.Count > 0)
					{
						res.Errors.ForEach(x => ModelState.AddModelError("", x.Message!));
						return View(userModel);
					}

				}
				else
				{
					ModelState.AddModelError("", "Geçersiz kullanýcý adý veya þifre");
					return View();
				}
			


			return View();

		}

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            Response.Cookies.Delete("Adi");
            Response.Cookies.Delete("AdiSoyadi");

            _logger.LogInformation(" Çýkýþ iþlemi gerçekleþti (" + Response.Cookies.ToString() + ")");

            return RedirectToAction("Index");
        }



        [Authorize(Roles = "Admin")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {  Title = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
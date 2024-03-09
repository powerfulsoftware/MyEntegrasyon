using Microsoft.AspNetCore.Mvc;
using MyEntegrasyon.Data;
using MyEntegrasyon.Models;
using MyEntegrasyon.Models.Nebim;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace MyEntegrasyon.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyContext _context;
        private readonly ILogger<HomeController> _logger;
        private string Connect_Url = "http://95.70.226.23:1515/(S(fjcangjis432kyhkvtblqxia))/IntegratorService/Connect";
        public HomeController(MyContext myContext, ILogger<HomeController> logger)
        {
            _context = myContext;
            _logger = logger;
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
    }
}
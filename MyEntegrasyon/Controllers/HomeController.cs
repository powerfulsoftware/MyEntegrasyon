using Microsoft.AspNetCore.Mvc;
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
        private readonly ILogger<HomeController> _logger;
        private string Connect_Url = "http://95.70.226.23:1515/(S(fjcangjis432kyhkvtblqxia))/IntegratorService/Connect";
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        

      


        public async Task<IActionResult> Index()
        {
            
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            var result = await client.GetStringAsync(Connect_Url);
            var gelen = JsonConvert.DeserializeObject<ConnectJson>(result)!;



            HttpClient client2 = new HttpClient();
            client2.DefaultRequestHeaders.Add("Accept", "application/json");
            var result2 = await client2.GetStringAsync("http://95.70.226.23:1515/(S("+ gelen.SessionID  + "))/IntegratorService/RunProc?{'ProcName':'KidaIkasEntegrasyonSon','Parameters':[]}");
            
            // result2 = result2.Substring(1, result2.Length - 1).Substring(0, result2.Length - 2);

            var gelen2 = JsonConvert.DeserializeObject<List<Parameter>>(result2, new JsonSerializerSettings() { Culture = CultureInfo.InvariantCulture, FloatParseHandling = FloatParseHandling.Double })!;



            return View(gelen2);
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
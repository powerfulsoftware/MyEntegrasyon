using Microsoft.AspNetCore.Mvc;
using MyEntegrasyon.Models;
using System;
using System.Diagnostics;
using System.Net;

namespace MyEntegrasyon.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string? url = "";
            url = "http://95.70.226.23:1515/(S(fjcangjis432kyhkvtblqxia))/IntegratorService/Connect";
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "GET";
            webRequest.ContentType = "application/json";
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 5.1; rv:28.0) Gecko/20100101 Firefox/28.0";
            webRequest.ContentLength = 0;

            var webResponse = (HttpWebResponse)webRequest.GetResponse();

            if (webResponse.StatusCode != HttpStatusCode.OK) // olumsuzsa
            {

                var gelen = webResponse.Headers.ToString();
            }
            using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
            {
                Console.WriteLine(reader.ReadToEnd());
                reader.Close();
                webRequest.Abort();
            }


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
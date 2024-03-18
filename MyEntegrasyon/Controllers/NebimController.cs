using Microsoft.AspNetCore.Mvc;
using MyEntegrasyon.Models.Nebim;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http;

namespace MyEntegrasyon.Controllers
{
    public class NebimController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;


        public NebimController(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }



      
        public async Task<IActionResult> Products()
        {
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            var result = await _httpClient.GetStringAsync(_configuration.GetSection("UrlServiceSettings:ConnectAdress").Value!);  // Connect_Url
            ConnectJson connectJson = JsonConvert.DeserializeObject<ConnectJson>(result)!;

            //////////////////////////

            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            var result2 = await _httpClient.GetStringAsync((_configuration.GetSection("UrlServiceSettings:IntegratorService").Value!).Replace("connectJson.SessionID", connectJson.SessionID));

            List<Parameter> parameters = JsonConvert.DeserializeObject<List<Parameter>>(result2, new JsonSerializerSettings() { Culture = CultureInfo.InvariantCulture, FloatParseHandling = FloatParseHandling.Double })!;

            ViewBag.SessionID = "http://95.70.226.23:1515/(S(" + connectJson.SessionID + "))";

            return View(parameters);
        }
    }
}

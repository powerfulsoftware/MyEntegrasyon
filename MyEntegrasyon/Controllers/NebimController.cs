using Microsoft.AspNetCore.Mvc;
using MyEntegrasyon.Models.Nebim;
using Newtonsoft.Json;
using System.Globalization;

namespace MyEntegrasyon.Controllers
{
    public class NebimController : Controller
    {
        private string Connect_Url = "http://95.70.226.23:1515/(S(fjcangjis432kyhkvtblqxia))/IntegratorService/Connect";
        public async Task<IActionResult> Products()
        {


          //  NebimV3\NebimV3Resimler\101A01267\ColorPhotos\101A01267_030.jpg




            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            var result = await client.GetStringAsync(Connect_Url);
            ConnectJson connectJson = JsonConvert.DeserializeObject<ConnectJson>(result)!;

            HttpClient client2 = new HttpClient();
            client2.DefaultRequestHeaders.Add("Accept", "application/json");
            var result2 = await client2.GetStringAsync("http://95.70.226.23:1515/(S(" + connectJson.SessionID + "))/IntegratorService/RunProc?{'ProcName':'KidaIkasEntegrasyonSon','Parameters':[]}");

            // result2 = result2.Substring(1, result2.Length - 1).Substring(0, result2.Length - 2);

            List<Parameter> parameters = JsonConvert.DeserializeObject<List<Parameter>>(result2, new JsonSerializerSettings() { Culture = CultureInfo.InvariantCulture, FloatParseHandling = FloatParseHandling.Double })!;


            ViewBag.SessionID = "http://95.70.226.23:1515/(S(" + connectJson.SessionID + "))";



            return View(parameters);

        }
    }
}

using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQL.SystemTextJson;
using Microsoft.AspNetCore.Mvc;
using MyEntegrasyon.Data;
using MyEntegrasyon.Models.Myikas;
using MyEntegrasyon.Models.Nebim;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Net.Http;
using System.Web;

namespace MyEntegrasyon.Controllers
{
    public class IkasController : Controller
    {

        private readonly MyContext _context;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;


        private static readonly string _clientId = "1116983b-a867-46f7-9cb8-c93be9f0c480";    // Application (client) ID
        private static readonly string _secret = "s_h12iRZnUw7BEgapR0IZT9Cdd6f6f7a7bf2f74c6e8ee27202d70c95db";  // Secret value 
        private static readonly string _url = "https://agevadigital1.myikas.com/api/admin/oauth/token";
        private static readonly string _endPoind = "https://api.myikas.com/api/v1/admin/graphql";
        private static readonly string _method = "POST";
        private static string _access_token = string.Empty;






        public IkasController(MyContext context, IConfiguration configuration, HttpClient httpClient)
        {
            _context = context;
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<bool> YeniUnrunEkle()
        {





            ///////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////
            ////////////////     NEBİM KISMI       /////////////////



            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            var result = await _httpClient.GetStringAsync(_configuration.GetSection("UrlServiceSettings:ConnectAdress").Value!);  // Connect_Url
            ConnectJson connectJson = JsonConvert.DeserializeObject<ConnectJson>(result)!;

            //////////////////////////

            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            var result2 = await _httpClient.GetStringAsync((_configuration.GetSection("UrlServiceSettings:IntegratorService").Value!).Replace("connectJson.SessionID", connectJson.SessionID));

            List<Parameter> parameters = JsonConvert.DeserializeObject<List<Parameter>>(result2, new JsonSerializerSettings() { Culture = CultureInfo.InvariantCulture, FloatParseHandling = FloatParseHandling.Double })!;





            ///////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////
            ////////////////     İKAS KISMI       /////////////////

            // GetAccessToken();


           







           


            using (var client = new GraphQLHttpClient(_endPoind, new NewtonsoftJsonSerializer()))
            {

                var content3 = new StringContent("grant_type=client_credentials&scope=https://api.businesscentral.dynamics.com/.default&client_id="
              + HttpUtility.UrlEncode(_clientId) + "&client_secret=" + HttpUtility.UrlEncode(_secret));
                content3.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
                var response3 = await _httpClient.PostAsync(_url, content3);
                if (response3.IsSuccessStatusCode)
                {
                    JObject result3 = JObject.Parse(await response3.Content.ReadAsStringAsync());
                    _access_token = result3["access_token"]!.ToString();
                }


                client.HttpClient.DefaultRequestHeaders.Add("Authorization", $"bearer {_access_token}");


                //client.HttpClient.DefaultRequestHeaders.Add("User-Agent", "SharpenAboutBox");
                //client.HttpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");


               

                // nebimden gelen json dosyasını Root modeline eklenecek sonra ikasın istediği json formatına çevrilecek
               

                foreach (var parameter in parameters)
                {
                    GraphQLResponse<dynamic> gelen = new GraphQLResponse<dynamic>();
                    GraphQLRequest request = new GraphQLRequest();

                    // fiyatlar
                    List<Price> _prices = new List<Price>(); // fiyatlar
                    _prices.Add(new Price {
                        buyPrice= (float)Convert.ToDouble(parameter.AlisFiyati, CultureInfo.InvariantCulture),
                        currency = parameter.CurrencyCode!, 
                        discountPrice= (float)Convert.ToDouble(parameter.Price5),  
                        sellPrice = (float)Convert.ToDouble(parameter.Price1) 
                    });

                 
                    // varyantlar
                    List<Variant> _variants = new List<Variant>(); // varyantlar
                    _variants.Add(new Variant {
                        barcodeList = new string[] { parameter.Barcode! },
                        isActive = true,
                        SKU = parameter.ItemCode,
                        prices = _prices
                        
                    });

                    // satış Kanalları
                    List<SalesChannel> _salesChannels = new List<SalesChannel>(); // satış Kanalları
                    _salesChannels.Add(new SalesChannel { id = "12345", status = "PASSIVE" });

                    Input input = new Input();
                    input.name = parameter.ItemName;
                    input!.type = "PHYSICAL"; // Bu kısım sorulacak
                    input!.shortDescription = parameter.ItemDesc;
                    // input.totalStock = (float)Convert.ToDouble(parameter.Qty);
                    input.variants = _variants;
                  //  input!.brandId = parameter.BrandDesc;
                    // input!.brand = parameter.BrandDesc;
                  //  input!.categoryIds = parameter.Cat01Desc;
                    // input!.categoryIds = new categories { id = parameter.Cat01Code, name = parameter.Cat01Desc, parentId="Tekstil" };
                    input!.salesChannels = _salesChannels;
                    input!.salesChannelIds = "12345";


                    Root root = new Root();
                    root.input = input;
                  



                    // nebimden gelen json dosyasını Root modeline eklenecek sonra ikasın istediği json formatına çevrilecek
                    // yada nebimden gelen json dosyasını Root modeline eklenecek sonra ikasın istediği Variables alanına eklenecek --- Önemli

                  //  var requestJson = "";   // Models.GraphQLQueries.saveProduct89; // Nebimden gelen veriler Root formatında json verisi olarak bu kısıma eklenecek.
                  //  var inputs = new GraphQLSerializer().Deserialize<Root>(requestJson);


                    request.Query = _context.Islem.Where(x => x.IslemAdi == "ikasSaveProduct").FirstOrDefault()!.JsonDesen!.Pattern!;   // Desen ( Pattern )
                    request.Variables = root;
                    request.OperationName = "Mutation";


                    gelen = await client.SendMutationAsync<dynamic>(request);
                    // return await client.SendQueryAsync<dynamic>(request);
                }







            }

            return true;


        }


//        public static string saveProduct89 = @"{
//  ""input"": {
//    ""name"": ""Yeni Yeni Yeni 12"",
//    ""type"": ""PHYSICAL"",
//    ""variants"": [
//      {
//        ""isActive"": true,
//        ""prices"": [
//          {
//            ""sellPrice"": 100
//          }
//        ]
//      }
//    ],
//    ""salesChannels"": [
//      {
//        ""id"": ""1234"",
//        ""status"": ""PASSIVE""
//      }
//    ],
//    ""salesChannelIds"": ""1234""
//  }
//}";




//    }




    private async void GetAccessToken()
    {
            
            try
            {
               // HttpClient httpClient = new HttpClient();
                var content = new StringContent("grant_type=client_credentials&scope=https://api.businesscentral.dynamics.com/.default&client_id="
                + HttpUtility.UrlEncode(_clientId) + "&client_secret=" + HttpUtility.UrlEncode(_secret));
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
                var response = await _httpClient.PostAsync(_url, content);
                if (response.IsSuccessStatusCode)
                {
                    JObject result = JObject.Parse(await response.Content.ReadAsStringAsync());
                    _access_token = result["access_token"]!.ToString();
                }
            }
            catch (Exception ex)
            {

               string message = ex.Message;
            }

        
    }

}
}

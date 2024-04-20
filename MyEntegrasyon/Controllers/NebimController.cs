using Azure.Core;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEntegrasyon.Data;
using MyEntegrasyon.Models.Myikas;
using MyEntegrasyon.Models.Nebim;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Net.Http;
using System.Web;
using System.Data;

namespace MyEntegrasyon.Controllers
{
    public class NebimController : Controller
    {
        private readonly ILogger<TanimlamaController> _logger;
        private readonly MyContext _context;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;



        private static readonly string _clientId = "1116983b-a867-46f7-9cb8-c93be9f0c480";    // Application (client) ID
        private static readonly string _secret = "s_h12iRZnUw7BEgapR0IZT9Cdd6f6f7a7bf2f74c6e8ee27202d70c95db";  // Secret value 
        private static readonly string _url = "https://agevadigital1.myikas.com/api/admin/oauth/token";
        private static readonly string _endPoind = "https://api.myikas.com/api/v1/admin/graphql";
        private static readonly string _method = "POST";
        private static string _access_token = string.Empty;



        public NebimController(ILogger<TanimlamaController> logger, MyContext context, IConfiguration configuration, HttpClient httpClient)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
            _httpClient = httpClient;
        }

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


        public async Task<IActionResult> SistemProducts()
        {
            List<Data.Entities.Product> products = await _context.Product.ToListAsync();
            return View(products);
        }
        public async Task<IActionResult> IkasProducts()
        {
            GetAccessToken();
            Models.Myikas.ListProduct.Root Gonder = new Models.Myikas.ListProduct.Root();
            
            // RootProductBrand BrandList = new RootProductBrand();
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

                /////////  Ürün Listesi
                GraphQLResponse<Models.Myikas.ListProduct.Root> gelen_Products = new GraphQLResponse<Models.Myikas.ListProduct.Root>();
                var request_list = new GraphQLRequest()
                {
                    Query = _context.Islem.Where(x => x.IslemAdi == "ListProduct").FirstOrDefault()!.JsonDesen!.Pattern!   // Desen ( Pattern )                                                                                                         
                };
                gelen_Products = await client.SendQueryAsync<Models.Myikas.ListProduct.Root>(request_list);


                Gonder = gelen_Products.Data;

                // table = JsonConvert.DeserializeObject<DataTable>(gelen_Products.Data);

                // ProductList = gelen_Products.Data;


                /////////  Ürün Listesi
                GraphQLResponse<Models.Myikas.VariantC.Root> gelen_VariantTypes = new GraphQLResponse<Models.Myikas.VariantC.Root>();
                var request_VariantTypes = new GraphQLRequest()
                {
                    Query = _context.Islem.Where(x => x.IslemAdi == "ListVariantTypeFull").FirstOrDefault()!.JsonDesen!.Pattern!   // Desen ( Pattern )                                                                                                         
                };
                gelen_VariantTypes = await client.SendQueryAsync<Models.Myikas.VariantC.Root>(request_VariantTypes);


               var Gonder_VariantTypes = gelen_VariantTypes.Data;





            }

            return View(Gonder);
        }
        



    }
}

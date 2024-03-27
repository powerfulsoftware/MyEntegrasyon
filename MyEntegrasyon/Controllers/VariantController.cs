using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQL;
using Microsoft.AspNetCore.Mvc;
using MyEntegrasyon.Data;
using MyEntegrasyon.Models.Myikas.VariantC;
using Newtonsoft.Json.Linq;
using System.Web;

namespace MyEntegrasyon.Controllers
{
    public class VariantController : Controller
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

        public VariantController(MyContext context, IConfiguration configuration, HttpClient httpClient)
        {
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
        public async Task<IActionResult> Index()
        {
          

            List<ListVariantType> listVariantTypes = new List<ListVariantType>();
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

                /////////  MARKA LİSTESİ   /////////
                GraphQLResponse<Root> gelen_Brand = new GraphQLResponse<Root>();
                var request_Brand = new GraphQLRequest()
                {
                    Query = _context.Islem.Where(x => x.IslemAdi == "listVariantType").FirstOrDefault()!.JsonDesen!.Pattern!   // Desen ( Pattern )                                                                                                         
                };
                gelen_Brand = await client.SendQueryAsync<Root>(request_Brand);
                listVariantTypes = gelen_Brand.Data.listVariantType;
            }

            return View(listVariantTypes);
        }


        public async Task Delete()
        {
            List<string> list = new List<string>();
            List<ListVariantType> listVariantTypes = new List<ListVariantType>();
            using (var client = new GraphQLHttpClient(_endPoind, new NewtonsoftJsonSerializer()))
            {

                var content4 = new StringContent("grant_type=client_credentials&scope=https://api.businesscentral.dynamics.com/.default&client_id="
              + HttpUtility.UrlEncode(_clientId) + "&client_secret=" + HttpUtility.UrlEncode(_secret));
                content4.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
                var response3 = await _httpClient.PostAsync(_url, content4);
                if (response3.IsSuccessStatusCode)
                {
                    JObject result3 = JObject.Parse(await response3.Content.ReadAsStringAsync());
                    _access_token = result3["access_token"]!.ToString();
                }

                client.HttpClient.DefaultRequestHeaders.Add("Authorization", $"bearer {_access_token}");

                /////////  MARKA LİSTESİ   /////////
                GraphQLResponse<Root> gelen_Brand = new GraphQLResponse<Root>();
                var request_Brand = new GraphQLRequest()
                {
                    Query = _context.Islem.Where(x => x.IslemAdi == "listVariantType").FirstOrDefault()!.JsonDesen!.Pattern!   // Desen ( Pattern )                                                                                                         
                };
                gelen_Brand = await client.SendQueryAsync<Root>(request_Brand);
                listVariantTypes = gelen_Brand.Data.listVariantType;


                foreach (var item in listVariantTypes!)
                {
                    list.Add(item.id!);
                }




                ////////  Silme işlemi ////////////






                 // deleteVariantTypeList

                MyEntegrasyon.Models.Myikas.SaveVariant.Root _root = new Models.Myikas.SaveVariant.Root();
                _root.idList = list;

                GraphQLResponse<MyEntegrasyon.Models.Myikas.SaveVariant.Root> gelen_VariantDelete = new GraphQLResponse<MyEntegrasyon.Models.Myikas.SaveVariant.Root>();
                var request_VariantDelete = new GraphQLRequest()
                {
                    Query = _context.Islem.Where(x => x.IslemAdi == "deleteVariantTypeList").FirstOrDefault()!.JsonDesen!.Pattern!,   // Desen ( Pattern )
                    Variables = _root
                };

                try
                {
                    gelen_VariantDelete = await client.SendQueryAsync<MyEntegrasyon.Models.Myikas.SaveVariant.Root>(request_VariantDelete);
                    bool VariantDelete = gelen_VariantDelete.Data.deleteVariantTypeList;
                   
                }
                catch (Exception ex)
                {
                    string message = ex.Message;

                }



            }



            Response.Redirect("Index");


        }



    }
}

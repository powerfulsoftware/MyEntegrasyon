using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQL;
using Microsoft.AspNetCore.Mvc;
using MyEntegrasyon.Data;
using Newtonsoft.Json.Linq;
using System.Web;
using MyEntegrasyon.Models.Myikas.Category;

namespace MyEntegrasyon.Controllers
{
    public class CategoryController : Controller
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

        public CategoryController(MyContext context, IConfiguration configuration, HttpClient httpClient)
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
            List<ListCategory> listCategory = new List<ListCategory>();
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

                /////////  tCategory LİSTESİ   /////////
                GraphQLResponse<Root> gelen_category = new GraphQLResponse<Root>();
                var request_category = new GraphQLRequest()
                {
                    Query = _context.Islem.Where(x => x.IslemAdi == "listCategory").FirstOrDefault()!.JsonDesen!.Pattern!   // Desen ( Pattern )                                                                                                         
                };
                gelen_category = await client.SendQueryAsync<Root>(request_category);
                listCategory = gelen_category.Data.listCategory!;
            }

            return View(listCategory);
        }








        public async Task Delete()
        {
            List<string> list = new List<string>();
            List<ListCategory> listCategory = new List<ListCategory>();
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

                /////////  tCategory LİSTESİ   /////////
                GraphQLResponse<Root> gelen_category = new GraphQLResponse<Root>();
                var request_category = new GraphQLRequest()
                {
                    Query = _context.Islem.Where(x => x.IslemAdi == "listCategory").FirstOrDefault()!.JsonDesen!.Pattern!   // Desen ( Pattern )                                                                                                         
                };
                gelen_category = await client.SendQueryAsync<Root>(request_category);
                listCategory = gelen_category.Data.listCategory!;


                foreach (var item in listCategory!)
                {
                    list.Add(item.id!);
                }




                ////////  Silme işlemi ////////////






                // deletecategoryList

                MyEntegrasyon.Models.Myikas.Category.Root _root = new Models.Myikas.Category.Root();
                _root.idList = list;

                GraphQLResponse<MyEntegrasyon.Models.Myikas.Category.Root> gelen_CategoryDelete = new GraphQLResponse<MyEntegrasyon.Models.Myikas.Category.Root>();
                var request_CategoryDelete = new GraphQLRequest()
                {
                    Query = _context.Islem.Where(x => x.IslemAdi == "deleteCategoryList").FirstOrDefault()!.JsonDesen!.Pattern!,   // Desen ( Pattern )
                    Variables = _root
                };

                try
                {
                    gelen_CategoryDelete = await client.SendQueryAsync<MyEntegrasyon.Models.Myikas.Category.Root>(request_CategoryDelete);
                    bool CategoryDelete = gelen_CategoryDelete.Data.deleteCategoryList;

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

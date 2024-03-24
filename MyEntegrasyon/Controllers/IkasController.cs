using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQL.SystemTextJson;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic;
using MyEntegrasyon.Data;
using MyEntegrasyon.Models.Myikas;
using MyEntegrasyon.Models.Myikas.BrandAdd;
using MyEntegrasyon.Models.Myikas.Category;
using MyEntegrasyon.Models.Nebim;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Web;
using static System.Net.Mime.MediaTypeNames;
using Parameter = MyEntegrasyon.Models.Nebim.Parameter;

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


        public async Task<RootProductBrand> MarkaListesiGetir()
        {
            RootProductBrand BrandList = new RootProductBrand();    
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

                /////////  MARKA LİSTESİ
                GraphQLResponse<RootProductBrand> gelen_Brand = new GraphQLResponse<RootProductBrand>();
                var request_Brand = new GraphQLRequest()
                {
                    Query = _context.Islem.Where(x => x.IslemAdi == "GetBrands").FirstOrDefault()!.JsonDesen!.Pattern!   // Desen ( Pattern )                                                                                                         
                };
                gelen_Brand = await client.SendQueryAsync<RootProductBrand>(request_Brand);
                BrandList = gelen_Brand.Data;
            }

            return BrandList;
        }
        public async Task<string> YeniMarkaEkle(string name)
        {
           
            string ID = string.Empty;

            using (var client2 = new GraphQLHttpClient(_endPoind, new NewtonsoftJsonSerializer()))
            {

                client2.HttpClient.DefaultRequestHeaders.Add("Authorization", $"bearer {_access_token}");

                MyEntegrasyon.Models.Myikas.BrandAdd.Root _root = new Models.Myikas.BrandAdd.Root();
                MyEntegrasyon.Models.Myikas.BrandAdd.Input _input = new Models.Myikas.BrandAdd.Input();
                _input.name = name;
                _root.input = _input;

                // var requestJson = "{input:{name: " + item_product.BrandDesc!.Replace("İ","I") + "}}";
                //  var inputs = new GraphQLSerializer().Deserialize<MyEntegrasyon.Models.Myikas.BrandAdd.Root>(requestJson);


                GraphQLResponse<MyEntegrasyon.Models.Myikas.BrandAdd.Root> gelen_Brand = new GraphQLResponse<MyEntegrasyon.Models.Myikas.BrandAdd.Root>();
                var request_Brand = new GraphQLRequest()
                {
                    Query = _context.Islem.Where(x => x.IslemAdi == "saveProductBrand").FirstOrDefault()!.JsonDesen!.Pattern!,   // Desen ( Pattern )
                    Variables = _root
                };

                try
                {
                    gelen_Brand = await client2.SendQueryAsync<MyEntegrasyon.Models.Myikas.BrandAdd.Root>(request_Brand);
                    MyEntegrasyon.Models.Myikas.BrandAdd.Root saveProductBrand = gelen_Brand.Data;
                    ID = saveProductBrand.saveProductBrand!.id!;
                }
                catch (Exception ex)
                {
                    string message = ex.Message;

                }


            }

            return ID;
        }


        public async Task<List<MyEntegrasyon.Models.Myikas.Category.ListCategory>> ListCategory()
        {

         List<MyEntegrasyon.Models.Myikas.Category.ListCategory> kategoriListesi = new List<MyEntegrasyon.Models.Myikas.Category.ListCategory>();

            GraphQLResponse<MyEntegrasyon.Models.Myikas.Category.Root> gelen5 = new GraphQLResponse<MyEntegrasyon.Models.Myikas.Category.Root>();

            using (var client = new GraphQLHttpClient(_endPoind, new NewtonsoftJsonSerializer()))
            {
                client.HttpClient.DefaultRequestHeaders.Add("Authorization", $"bearer {_access_token}");
                //client.HttpClient.DefaultRequestHeaders.Add("User-Agent", "SharpenAboutBox");
                //client.HttpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

                var request5 = new GraphQLRequest()
                {

                    //listCategory 
                    Query = _context.Islem.Where(x => x.IslemAdi == "listCategory").FirstOrDefault()!.JsonDesen!.Pattern!   // Desen ( Pattern )    
                };
                gelen5 = await client.SendQueryAsync<MyEntegrasyon.Models.Myikas.Category.Root>(request5);
                 kategoriListesi = gelen5.Data.listCategory!;
            }

            // ViewBag.request = gelen.Data;

            return kategoriListesi;
        }


        public async Task<string> YeniKategoriEkle(string name)
        {

            string ID = string.Empty;

            using (var client2 = new GraphQLHttpClient(_endPoind, new NewtonsoftJsonSerializer()))
            {

                client2.HttpClient.DefaultRequestHeaders.Add("Authorization", $"bearer {_access_token}");

                MyEntegrasyon.Models.Myikas.Category.Root _root = new Models.Myikas.Category.Root();
                MyEntegrasyon.Models.Myikas.Category.Input _input = new Models.Myikas.Category.Input();
                _input.name = name;
                _root.input = _input;

                GraphQLResponse<MyEntegrasyon.Models.Myikas.Category.Root> gelen_Categori = new GraphQLResponse<MyEntegrasyon.Models.Myikas.Category.Root>();
                var request_Categori = new GraphQLRequest()
                {
                    Query = _context.Islem.Where(x => x.IslemAdi == "saveCategory").FirstOrDefault()!.JsonDesen!.Pattern!,   // Desen ( Pattern )
                    Variables = _root
                };

                try
                {
                    gelen_Categori = await client2.SendQueryAsync<MyEntegrasyon.Models.Myikas.Category.Root>(request_Categori);
                    MyEntegrasyon.Models.Myikas.Category.Root saveCategory = gelen_Categori.Data;
                    ID = saveCategory.saveCategory!.id!;
                }
                catch (Exception ex)
                {
                    string message = ex.Message;

                }


            }

            return ID;
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


           // Product _product = new Product();
            //ProductVariant _productVariant = new ProductVariant();

            List<Product> _products = new List<Product>();
            //List<ProductVariant> _productVariants = new List<ProductVariant>();
            foreach (var item in parameters)
            {
                string? Id = _products.Where(x => x.Id == item.ItemCode).FirstOrDefault()?.ItemCode;

                if (item.ItemCode != Id) // ItemCode lu ürün hiç yoksa 
                {


                    ProductVariant _productVariant = new ProductVariant() {

                        ProductID = item.ItemCode,
                        CurrencyCode = item.CurrencyCode,
                        Barcode = item.Barcode,
                        GenderCode = item.GenderCode,
                        ColorCode = item.ColorCode,
                        ColorDesc = item.ColorDesc,
                        ItemDimTypeCode = item.ItemDimTypeCode,
                        ItemDim1Code = item.ItemDim1Code,
                        ItemDim1Desc = item.ItemDim1Desc,
                        ItemDim2Code = item.ItemDim2Code,
                        ItemDim2Desc = item.ItemDim2Desc,
                        ItemDim3Code = item.ItemDim3Code,
                        ItemDim3Desc = item.ItemDim3Desc,
                        Qty = item.Qty,
                        Vat = item.Vat,
                        Price1 = item.Price1,
                        Price2 = item.Price2,
                        Price3 = item.Price3,
                        Price4 = item.Price4,
                        Price5 = item.Price5,
                        AlisFiyati = item.AlisFiyati,
                        ProductAtt10 = item.ProductAtt10,
                        ProductAtt10Desc = item.ProductAtt10Desc,
                        PAZARYERIISK = item.PAZARYERIISK,
                        N11_LST = item.N11_LST,
                        N11_IND = item.N11_IND,
                        AMAZON_LST = item.AMAZON_LST,
                        AMAZON_IND = item.AMAZON_IND,
                        CICEK_LST = item.CICEK_IND,
                        CICEK_IND = item.CICEK_IND,
                        GITTIGIDIYOR_LST = item.GITTIGIDIYOR_LST,
                        GITTIGIDIYOR_IND = item.GITTIGIDIYOR_IND,
                        HEPSIBURADA_LST = item.HEPSIBURADA_LST,
                        HEPSIBURADA_IND = item.HEPSIBURADA_IND,
                        MORHIPO_LST = item.MORHIPO_LST,
                        MORHIPO_IND = item.MORHIPO_IND,
                        PAZARAMA_LST = item.PAZARAMA_LST,
                        PAZARAMA_IND = item.PAZARAMA_IND,
                        TRENDYOL_LST = item.TRENDYOL_LST,
                        TRENDYOL_IND = item.TRENDYOL_IND,
                        BISIFIRAT_LST = item.BISIFIRAT_LST,
                        BISIFIRAT_IND = item.BISIFIRAT_IND,
                        TTTURK_LST = item.TTTURK_LST,
                        TTTURK_IND = item.TTTURK_IND,
                        BREND_LST = item.BREND_LST,
                        BREND_IND = item.BREND_IND,
                        Image1 = item.Image1,
                        Image2 = item.Image2,
                        Image3 = item.Image3,
                        Image4 = item.Image4,
                        Image5 = item.Image5,
                        Image6 = item.Image6,
                        Image7 = item.Image7,
                        Image8 = item.Image8

                    };
                   

                    _products.Add(new Product
                    {
                        Id = item.ItemCode,
                        ItemCode = item.ItemCode,
                        ItemName = item.ItemName,
                        ItemDesc = item.ItemDesc,

                        Cat01Code = item.Cat01Code,
                        Cat01Desc = item.Cat01Desc,
                        Cat02Code = item.Cat02Code,
                        Cat02Desc = item.Cat02Desc,
                        Cat03Code = item.Cat03Code,
                        Cat03Desc = item.Cat03Desc,
                        Cat04Code = item.Cat04Code,
                        Cat04Desc = item.Cat04Desc,
                        Cat05Code = item.Cat05Code,
                        Cat05Desc = item.Cat05Desc,
                        Cat06Code = item.Cat06Code,
                        Cat06Desc = item.Cat06Desc,
                        Cat07Code = item.Cat07Code,
                        Cat07Desc = item.Cat07Desc,
                        BrandCode = item.BrandCode,
                        BrandDesc = item.BrandDesc,

                        ProductVariants = new List<ProductVariant>(){ _productVariant }

                    });

                }
                else // ItemCode lu ürün varsa. barkodu sorgula
                {
                    string? Barcode = _products.Where(x => x.ItemCode == item.ItemCode).FirstOrDefault()?.ProductVariants?.Where(x => x.Barcode == item.Barcode).FirstOrDefault()?.Barcode;

                   // string? Barcode = _productVariants.Where(x => x.Barcode == item.Barcode).FirstOrDefault()?.Barcode;


                    if (item.Barcode != Barcode)
                    {


                        ProductVariant _productVariant = new ProductVariant()
                        {

                            ProductID = item.ItemCode,
                            CurrencyCode = item.CurrencyCode,
                            Barcode = item.Barcode,
                            GenderCode = item.GenderCode,
                            ColorCode = item.ColorCode,
                            ColorDesc = item.ColorDesc,
                            ItemDimTypeCode = item.ItemDimTypeCode,
                            ItemDim1Code = item.ItemDim1Code,
                            ItemDim1Desc = item.ItemDim1Desc,
                            ItemDim2Code = item.ItemDim2Code,
                            ItemDim2Desc = item.ItemDim2Desc,
                            ItemDim3Code = item.ItemDim3Code,
                            ItemDim3Desc = item.ItemDim3Desc,
                            Qty = item.Qty,
                            Vat = item.Vat,
                            Price1 = item.Price1,
                            Price2 = item.Price2,
                            Price3 = item.Price3,
                            Price4 = item.Price4,
                            Price5 = item.Price5,
                            AlisFiyati = item.AlisFiyati,
                            ProductAtt10 = item.ProductAtt10,
                            ProductAtt10Desc = item.ProductAtt10Desc,
                            PAZARYERIISK = item.PAZARYERIISK,
                            N11_LST = item.N11_LST,
                            N11_IND = item.N11_IND,
                            AMAZON_LST = item.AMAZON_LST,
                            AMAZON_IND = item.AMAZON_IND,
                            CICEK_LST = item.CICEK_IND,
                            CICEK_IND = item.CICEK_IND,
                            GITTIGIDIYOR_LST = item.GITTIGIDIYOR_LST,
                            GITTIGIDIYOR_IND = item.GITTIGIDIYOR_IND,
                            HEPSIBURADA_LST = item.HEPSIBURADA_LST,
                            HEPSIBURADA_IND = item.HEPSIBURADA_IND,
                            MORHIPO_LST = item.MORHIPO_LST,
                            MORHIPO_IND = item.MORHIPO_IND,
                            PAZARAMA_LST = item.PAZARAMA_LST,
                            PAZARAMA_IND = item.PAZARAMA_IND,
                            TRENDYOL_LST = item.TRENDYOL_LST,
                            TRENDYOL_IND = item.TRENDYOL_IND,
                            BISIFIRAT_LST = item.BISIFIRAT_LST,
                            BISIFIRAT_IND = item.BISIFIRAT_IND,
                            TTTURK_LST = item.TTTURK_LST,
                            TTTURK_IND = item.TTTURK_IND,
                            BREND_LST = item.BREND_LST,
                            BREND_IND = item.BREND_IND,
                            Image1 = item.Image1,
                            Image2 = item.Image2,
                            Image3 = item.Image3,
                            Image4 = item.Image4,
                            Image5 = item.Image5,
                            Image6 = item.Image6,
                            Image7 = item.Image7,
                            Image8 = item.Image8

                        };

                        Product product =  _products.Where(x => x.Id == item.ItemCode).FirstOrDefault()!;
                        product.ProductVariants!.Add(_productVariant);

                        
                    }



                }


            }




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



                // Marka Listesi
                RootProductBrand _BrandList = await MarkaListesiGetir();
                // kategori Listesi
                List<MyEntegrasyon.Models.Myikas.Category.ListCategory> _KategoriListesi = await ListCategory();





                foreach (var item_product in _products)
                {
                    GraphQLResponse<dynamic> gelen = new GraphQLResponse<dynamic>();
                    GraphQLRequest request = new GraphQLRequest();

                    // fiyatlar
                    List<Price> _prices = new List<Price>(); // fiyatlar

                    // Varyant değeri kimliklerinin listesi.
                    List<VariantValue> _variantValueIds = new List<VariantValue>(); // Varyant değeri kimliklerinin listesi.

                    // varyantlar
                    List<Variant> _variants = new List<Variant>(); // varyantlar
                    // satış Kanalları
                    List<SalesChannel> _salesChannels = new List<SalesChannel>(); // satış Kanalları
                    foreach (var item_Variant in item_product.ProductVariants!)
                    {
                        // Marka adı varmı? kontol ediyoruz. Yoksa oluşturacağız.
                        bool BuMarkaAdiVarMi = _BrandList.listProductBrand!.Where(x => x.name == item_product.BrandDesc).ToList().Count > 0;
                        if(!BuMarkaAdiVarMi)// marka oluşturulacak
                        {

                            MyEntegrasyon.Models.Myikas.BrandAdd.Root _root = new Models.Myikas.BrandAdd.Root();
                            MyEntegrasyon.Models.Myikas.BrandAdd.Input _input = new Models.Myikas.BrandAdd.Input();
                            _input.name = item_product.BrandDesc;
                            _root.input = _input;

                            // var requestJson = "{input:{name: " + item_product.BrandDesc!.Replace("İ","I") + "}}";
                            //  var inputs = new GraphQLSerializer().Deserialize<MyEntegrasyon.Models.Myikas.BrandAdd.Root>(requestJson);


                            GraphQLResponse<MyEntegrasyon.Models.Myikas.BrandAdd.Root> gelen_Brand = new GraphQLResponse<MyEntegrasyon.Models.Myikas.BrandAdd.Root>();
                            var request_Brand = new GraphQLRequest()
                            {
                                Query = _context.Islem.Where(x => x.IslemAdi == "saveProductBrand").FirstOrDefault()!.JsonDesen!.Pattern!,   // Desen ( Pattern )
                                Variables = _root
                            };

                            try
                            {
                                gelen_Brand = await client.SendQueryAsync<MyEntegrasyon.Models.Myikas.BrandAdd.Root>(request_Brand);
                                MyEntegrasyon.Models.Myikas.BrandAdd.Root saveProductBrand = gelen_Brand.Data;
                                string ID = saveProductBrand.saveProductBrand!.id!;
                            }
                            catch (Exception ex)
                            {
                                string message = ex.Message;

                            }
                           // var gelendeger =  YeniMarkaEkle(item_product.BrandDesc!);
                        }
                        // Kategori varmı? kontol ediyoruz. Yoksa oluşturacağız.
                        bool BuKategoriVarMi = _KategoriListesi!.Where(x => x.name == item_product.Cat01Desc).ToList().Count > 0;
                        if (!BuKategoriVarMi)// Kategori oluşturulacak
                        {

                            MyEntegrasyon.Models.Myikas.Category.Root _root = new Models.Myikas.Category.Root();
                            MyEntegrasyon.Models.Myikas.Category.Input _input = new Models.Myikas.Category.Input();
                            _input.name = item_product.Cat01Desc;
                            _root.input = _input;

                            GraphQLResponse<MyEntegrasyon.Models.Myikas.Category.Root> gelen_Categori = new GraphQLResponse<MyEntegrasyon.Models.Myikas.Category.Root>();
                            var request_Categori = new GraphQLRequest()
                            {
                                Query = _context.Islem.Where(x => x.IslemAdi == "saveCategory").FirstOrDefault()!.JsonDesen!.Pattern!,   // Desen ( Pattern )
                                Variables = _root
                            };

                            try
                            {
                                gelen_Categori = await client.SendQueryAsync<MyEntegrasyon.Models.Myikas.Category.Root>(request_Categori);
                                MyEntegrasyon.Models.Myikas.Category.Root saveCategory = gelen_Categori.Data;
                                string ID = saveCategory.saveCategory!.id!;
                            }
                            catch (Exception ex)
                            {
                                string message = ex.Message;

                            }
                            // var gelendeger = YeniKategoriEkle(item_product.Cat01Desc!);
                        }




                        // fiyatlar
                        _prices.Add(new Price
                        {
                           // buyPrice = (float)Convert.ToDouble(item_Variant.AlisFiyati, CultureInfo.InvariantCulture),
                            //currency = item_Variant.CurrencyCode!,
                           // discountPrice = (float)Convert.ToDouble(item_Variant.Price5),
                            sellPrice = (float)Convert.ToDouble(item_Variant.Price1)
                        });
                        _variantValueIds.Add(new VariantValue{

                            variantTypeId = item_Variant.ItemDimTypeCode.ToString(),
                            variantValueId= item_Variant.ItemDim1Code
                        });

                        // varyantlar
                        _variants.Add(new Variant
                        {
                           // barcodeList = new string[] { item_Variant.Barcode! },
                            isActive = true,
                          //  SKU = "",
                            //SKU = item_product.ItemCode!,
                            prices = _prices,
                           // variantValueIds = _variantValueIds

                        });

                        // satış Kanalları
                        _salesChannels.Add(new SalesChannel { id = "12345", status = "PASSIVE" });


                       
                    }

                    Models.Myikas.Input input = new Models.Myikas.Input();

                    // input.Id = item_product.ItemCode;
                    input.name = item_product.ItemName;
                    input!.type = "PHYSICAL"; // Bu kısım sorulacak
                    input!.shortDescription = item_product.ItemDesc;
                    // input.totalStock = (float)Convert.ToDouble(parameter.Qty);
                    input.variants = _variants;
                    //  input!.brandId = parameter.BrandDesc;
                    // input!.brand = parameter.BrandDesc;
                    //  input!.categoryIds = parameter.Cat01Desc;
                    // input!.categoryIds = new categories { id = parameter.Cat01Code, name = parameter.Cat01Desc, parentId="Tekstil" };
                    input!.salesChannels = _salesChannels;
                    input!.salesChannelIds = "12345";


                    Models.Myikas.Root root = new Models.Myikas.Root();
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

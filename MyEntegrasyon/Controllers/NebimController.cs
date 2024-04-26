﻿using Azure.Core;
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
using Serilog;
using MyEntegrasyon.BusinessLayer.Results;
using MyEntegrasyon.Data.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

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


        public async Task<IActionResult> AddDatabase()
        {
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            var result = await _httpClient.GetStringAsync(_configuration.GetSection("UrlServiceSettings:ConnectAdress").Value!);  // Connect_Url
            ConnectJson connectJson = JsonConvert.DeserializeObject<ConnectJson>(result)!;


            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            var result2 = await _httpClient.GetStringAsync((_configuration.GetSection("UrlServiceSettings:IntegratorService").Value!).Replace("connectJson.SessionID", connectJson.SessionID));

            List<Parameter> parameters = JsonConvert.DeserializeObject<List<Parameter>>(result2, new JsonSerializerSettings() { Culture = CultureInfo.InvariantCulture, FloatParseHandling = FloatParseHandling.Double })!;



            List<Models.Nebim.Product> _products = new List<Models.Nebim.Product>();
            //List<ProductVariant> _productVariants = new List<ProductVariant>();
            foreach (var item in parameters)
            {
                string? Id = _products.Where(x => x.Id == item.ItemCode).FirstOrDefault()?.ItemCode;

                if (item.ItemCode != Id) // ItemCode lu ürün hiç yoksa 
                {


                    Models.Nebim.ProductVariant _productVariant = new Models.Nebim.ProductVariant()
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


                    _products.Add(new Models.Nebim.Product
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

                        ProductVariants = new List<Models.Nebim.ProductVariant>() { _productVariant }

                    });

                }
                else // ItemCode lu ürün varsa. barkodu sorgula
                {
                    string? Barcode = _products.Where(x => x.ItemCode == item.ItemCode).FirstOrDefault()?.ProductVariants?.Where(x => x.Barcode == item.Barcode).FirstOrDefault()?.Barcode;

                    // string? Barcode = _productVariants.Where(x => x.Barcode == item.Barcode).FirstOrDefault()?.Barcode;


                    if (item.Barcode != Barcode)
                    {


                        Models.Nebim.ProductVariant _productVariant = new Models.Nebim.ProductVariant()
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

                        Models.Nebim.Product product = _products.Where(x => x.Id == item.ItemCode).FirstOrDefault()!;
                        product.ProductVariants!.Add(_productVariant);


                    }



                }
            }










            //////// Database Kaydet/////////////////////
            List<Data.Entities.Product> _productsNew = new List<Data.Entities.Product>();


            int ProductVariantID = 0;

            foreach (var item in _products)
            {

                List<Data.Entities.ProductVariant>? _ProductVariants = new List<Data.Entities.ProductVariant>();

                

                foreach (var item_Variant in item.ProductVariants!)
                {
                    ProductVariantID++;
                    _ProductVariants.Add(new Data.Entities.ProductVariant
                    {
                        Id = ProductVariantID.ToString(),
                        ProductID = item.Id,
                        CurrencyCode =item_Variant.CurrencyCode,
                        Barcode =item_Variant.Barcode,
                        GenderCode = item_Variant.GenderCode,
                        ColorCode = item_Variant.ColorCode,
                        ColorDesc = item_Variant.ColorDesc,
                        ItemDimTypeCode = item_Variant.ItemDimTypeCode,
                        ItemDim1Code = item_Variant.ItemDim1Code,
                        ItemDim1Desc = item_Variant.ItemDim1Desc,
                        ItemDim2Code = item_Variant.ItemDim2Code,
                        ItemDim2Desc = item_Variant.ItemDim2Desc,
                        ItemDim3Code = item_Variant.ItemDim3Code,
                        ItemDim3Desc = item_Variant.ItemDim3Desc,
                        Qty = item_Variant.Qty,
                        Vat = item_Variant.Vat,
                        Price1 = item_Variant.Price1,
                        Price2 = item_Variant.Price2,
                        Price3 = item_Variant.Price3,
                        Price4 = item_Variant.Price4,
                        Price5 = item_Variant.Price5,
                        AlisFiyati = item_Variant.AlisFiyati,
                        ProductAtt10 = item_Variant.ProductAtt10,
                        ProductAtt10Desc =  item_Variant.ProductAtt10Desc,
                        PAZARYERIISK = item_Variant.PAZARYERIISK,
                        N11_LST = item_Variant.N11_LST,
                        N11_IND = item_Variant.N11_IND,
                        AMAZON_LST = item_Variant.AMAZON_LST,
                        AMAZON_IND = item_Variant.AMAZON_IND,
                        CICEK_LST = item_Variant.CICEK_LST,
                        CICEK_IND = item_Variant.CICEK_IND,
                        GITTIGIDIYOR_LST = item_Variant.GITTIGIDIYOR_LST,
                        GITTIGIDIYOR_IND = item_Variant.GITTIGIDIYOR_IND,
                        HEPSIBURADA_LST = item_Variant.HEPSIBURADA_LST,
                        HEPSIBURADA_IND = item_Variant.HEPSIBURADA_IND,
                        MORHIPO_LST = item_Variant.MORHIPO_LST,
                        MORHIPO_IND = item_Variant.MORHIPO_IND,
                        PAZARAMA_LST = item_Variant.PAZARAMA_LST,
                        PAZARAMA_IND = item_Variant.PAZARAMA_IND,
                        TRENDYOL_LST = item_Variant.TRENDYOL_LST,
                        TRENDYOL_IND = item_Variant.TRENDYOL_IND,
                        BISIFIRAT_LST = item_Variant.BISIFIRAT_LST,
                        BISIFIRAT_IND = item_Variant.BISIFIRAT_IND,
                        TTTURK_LST = item_Variant.TTTURK_LST,
                        TTTURK_IND = item_Variant.TTTURK_IND,
                        BREND_LST = item_Variant.BREND_LST,
                        BREND_IND = item_Variant.BREND_IND,
                        Image1 = item_Variant.Image1,
                        Image2 = item_Variant.Image2,
                        Image3 = item_Variant.Image3,
                        Image4 = item_Variant.Image4,
                        Image5 = item_Variant.Image5,
                        Image6 = item_Variant.Image6,
                        Image7 = item_Variant.Image7,
                        Image8 = item_Variant.Image8,
                    });
                }
                
               

                _productsNew.Add(new Data.Entities.Product
                {
                    Id = item.Id,
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
                    ProductVariants = _ProductVariants


                });
            }


            BusinessLayerResult<List<Data.Entities.Product>> res = new BusinessLayerResult<List<Data.Entities.Product>>();
            res.Result = _productsNew;


           

            await _context.Product.AddRangeAsync(res.Result);
            _context.SaveChanges();


            /////// Eksik kategoriler eklenecek

            
            foreach (var item in _productsNew)
            {
                bool varMi= _context.Categories.Where(x=>x.Cat01Desc == item.Cat01Desc).Any();

                if (!varMi)
                {

                    Data.Entities.Category _newcategory = new Data.Entities.Category();
                    _newcategory.Cat01Code = item.Cat01Code;
                    _newcategory.Cat01Desc = item.Cat01Desc;


                    BusinessLayerResult<Data.Entities.Category> res_category = new BusinessLayerResult<Data.Entities.Category>();
                    res_category.Result = _newcategory;


                    await _context.Categories.AddAsync(res_category.Result);
                    _context.SaveChanges();
                }
            }
            /////// Eksik markalar eklenecek

            foreach (var item in _productsNew)
            {
                bool varMi = _context.Brands.Where(x => x.BrandDesc == item.BrandDesc).Any();

                if (!varMi)
                {

                    Data.Entities.Brand _newBrand = new Data.Entities.Brand();
                    _newBrand.BrandCode = item.BrandCode;
                    _newBrand.BrandDesc = item.BrandDesc;


                    BusinessLayerResult<Data.Entities.Brand> res_Brand = new BusinessLayerResult<Data.Entities.Brand>();
                    res_Brand.Result = _newBrand;


                    await _context.Brands.AddAsync(res_Brand.Result);
                    _context.SaveChanges();
                }
            }





            return RedirectToAction("SistemProducts");
        }



        
        public async Task<IActionResult> CategoryBrandAdd()
        {
            var _products = _context.Product.ToList();

            /////// Eksik kategoriler eklenecek


            foreach (var item in _products)
            {
                bool varMi = _context.Categories.Where(x => x.Cat01Desc == item.Cat01Desc).Any();

                if (!varMi)
                {

                    Data.Entities.Category _newcategory = new Data.Entities.Category();
                    _newcategory.Cat01Code = item.Cat01Code;
                    _newcategory.Cat01Desc = item.Cat01Desc;


                    BusinessLayerResult<Data.Entities.Category> res_category = new BusinessLayerResult<Data.Entities.Category>();
                    res_category.Result = _newcategory;


                    await _context.Categories.AddAsync(res_category.Result);
                    _context.SaveChanges();
                }
            }
            /////// Eksik markalar eklenecek

            foreach (var item in _products)
            {
                bool varMi = _context.Brands.Where(x => x.BrandDesc == item.BrandDesc).Any();

                if (!varMi)
                {

                    Data.Entities.Brand _newBrand = new Data.Entities.Brand();
                    _newBrand.BrandCode = item.BrandCode;
                    _newBrand.BrandDesc = item.BrandDesc;


                    BusinessLayerResult<Data.Entities.Brand> res_Brand = new BusinessLayerResult<Data.Entities.Brand>();
                    res_Brand.Result = _newBrand;


                    await _context.Brands.AddAsync(res_Brand.Result);
                    _context.SaveChanges();
                }
            }


            RootProductBrand BrandList = new RootProductBrand();
            BrandList = await MarkaListesiGetir();


            foreach (var item in BrandList.listProductBrand!)
            {
                bool varmi = _context.Brands.Where(x => x.BrandDesc == item.name).Any();
                if (varmi)
                {

                    Brand brand = _context.Brands.Where(x => x.BrandDesc == item.name).FirstOrDefault()!;
                    brand.ikasId = item.id;

                    BusinessLayerResult<Data.Entities.Brand> res_Brand = new BusinessLayerResult<Data.Entities.Brand>();
                    res_Brand.Result = brand;

                    _context.Brands.Update(res_Brand.Result);
                    _context.SaveChanges();
                }
            }


            ////////////////////////////////////////////////////



            List<MyEntegrasyon.Models.Myikas.Category.ListCategory> CategoryList = new List<MyEntegrasyon.Models.Myikas.Category.ListCategory>();
            CategoryList = await KategoriListesiGetir();



            foreach (var item in CategoryList!)
            {
                bool varmi = _context.Categories.Where(x => x.Cat01Desc == item.name).Any();
                if (varmi)
                {

                    Category category = _context.Categories.Where(x => x.Cat01Desc == item.name).FirstOrDefault()!;
                    category.ikasId = item.id;

                    BusinessLayerResult<Data.Entities.Category> res_Category = new BusinessLayerResult<Data.Entities.Category>();
                    res_Category.Result = category;

                    _context.Categories.Update(res_Category.Result);
                    _context.SaveChanges();
                }
                else
                {
                    string ID = await YeniKategoriEkle(item.name!); // yeni kategori ekle 

                    /////////////////////

                    Category category = _context.Categories.Where(x => x.Cat01Desc == item.name).FirstOrDefault()!;
                    category.ikasId = ID;

                    BusinessLayerResult<Data.Entities.Category> res_Category = new BusinessLayerResult<Data.Entities.Category>();
                    res_Category.Result = category;

                    _context.Categories.Update(res_Category.Result);
                    _context.SaveChanges();

                }
            }




            return RedirectToAction("SistemProducts");
        }


        public async Task<IActionResult> SistemProductVariant(string Id)
        {
            Data.Entities.Product product =  _context.Product.Where(x=>x.Id == Id).FirstOrDefault()!;
            return View(product);
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
        public async Task<List<MyEntegrasyon.Models.Myikas.Category.ListCategory>> KategoriListesiGetir()
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

    }
}

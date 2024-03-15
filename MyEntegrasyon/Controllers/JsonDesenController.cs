using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyEntegrasyon.BusinessLayer.Results;
using MyEntegrasyon.Data;
using MyEntegrasyon.Data.Entities;
using MyEntegrasyon.Models.Messages;
using MyEntegrasyon.Models.ViewModel;
using System.Text.Json;

namespace MyEntegrasyon.Controllers
{
    public class JsonDesenController : Controller
    {

        private readonly MyContext _context;

        public JsonDesenController(MyContext context)
        {
                _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult newDesen()
        {
            ViewBag.TypeId = new SelectList(newJSONviewModel.typelistes(), "Id", "deger");
            ViewBag.FirmaId = new SelectList(newJSONviewModel.firmalistes(), "Id", "deger");
            return View();
        }

        public IActionResult ikas()
        {
            return View();
        }
        public IActionResult Nebim()
        {
            return View();
        }



        public async Task<JsonResult> JsonNewDesen(string Name, string TypeId, string FirmaId, string Description, string Pattern)
        {
            try
            {
                JsonDesen jsonDesen = new JsonDesen();
                jsonDesen.Name = Name;
                jsonDesen.TypeId = TypeId;
                jsonDesen.FirmaId = FirmaId;
                jsonDesen.Description = Description;
                jsonDesen.Pattern = Pattern;

                BusinessLayerResult<JsonDesen> res = new BusinessLayerResult<JsonDesen>();
                res.Result = jsonDesen;


                await _context.jsonDesen!.AddAsync(res.Result);
                _context.SaveChanges();

                if (res.Errors.Count > 0) /// Hata varsa
                {
                    ErrorViewModel errorNotifyObj = new ErrorViewModel()
                    {
                        Items = res.Errors,
                        Title = "Json Desen Eklenemedi",
                        RedirectingUrl = "~/JsonDesen/newDesen"
                    };
                  
                    return Json(new { success = false, responseBaslik = "Hata Oluştu!", responseText = errorNotifyObj });
                }

                return Json(new { success = true, responseBaslik = "Tamamlandı", responseText = "" + " Başarıyla Oluşturuldu." });
            }
            catch (Exception ex)
            {
                string mesaj = ex.Message;
                return Json(new { success = false, responseBaslik = "Hata Oluştu!", responseText = "İşlem sırasında hata oluştu." });
            }
        }
        public IActionResult JsonDesenler()
        {
            List<JsonDesen> list = _context.jsonDesen.ToList();

            return View(list);
        }
        


    }
}

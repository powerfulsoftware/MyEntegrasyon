using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyEntegrasyon.BusinessLayer.Results;
using MyEntegrasyon.Data;
using MyEntegrasyon.Data.Entities;
using MyEntegrasyon.Models.Messages;
using MyEntegrasyon.Models.ViewModel;

namespace MyEntegrasyon.Controllers
{
    [Authorize(Roles = "Admin")]
    public class IslemController : Controller
    {
        private readonly MyContext _context;

        public IslemController(MyContext context)
        {
            _context = context;      
        }

        public IActionResult NewIslem()
        {
            List<JsonDesen> jsonDesens = _context.jsonDesen.ToList();

      

            ViewBag.JsonDesenId = new SelectList(jsonDesens, "Id", "FullName");
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> NewIslem(string IslemAdi, int JsonDesenId, string Aciklama)
        {
            try
            {
                Islem islem = new Islem();
                islem.IslemAdi = IslemAdi;
                islem.JsonDesenId = JsonDesenId;
                islem.Aciklama = Aciklama;



                BusinessLayerResult<Islem> res = new BusinessLayerResult<Islem>();
                res.Result = islem;


                await _context.Islem.AddAsync(res.Result);
                _context.SaveChanges();

                if (res.Errors.Count > 0) /// Hata varsa
                {
                    ErrorViewModel errorNotifyObj = new ErrorViewModel()
                    {
                        Items = res.Errors,
                        Title = "İşlem Eklenemedi",
                        RedirectingUrl = "~/islem/NewIslem"
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
        public IActionResult ikas()
        {
            List<Islem> res = _context.Islem.Where(x=>x.JsonDesen!.FirmaId=="Ikas").ToList();
            return View(res);
        }
        public IActionResult Nebim()
        {
            List<Islem> res = _context.Islem.Where(x => x.JsonDesen!.FirmaId == "Nebim").ToList();
            return View(res);
        }
        public IActionResult islemler()
        {
            List<Islem> res = _context.Islem.ToList();
            return View(res);
        }

        public IActionResult IslemEdit(int Id)
        {
            Islem res = _context.Islem.Where(x=>x.Id == Id).FirstOrDefault()!;

            List<JsonDesen> jsonDesens = _context.jsonDesen.ToList();

            ViewBag.JsonDesenId = new SelectList(jsonDesens, "Id", "FullName", res.JsonDesenId);

            return View(res);
        }

        [HttpPost]
        public JsonResult IslemEdit(int Id, string IslemAdi, int JsonDesenId, string Aciklama)
        {
            try
            {
                Islem islem = _context.Islem.Where(x=>x.Id == Id).FirstOrDefault()!;
                islem.IslemAdi = IslemAdi;
                islem.JsonDesenId = JsonDesenId;
                islem.Aciklama = Aciklama;



                BusinessLayerResult<Islem> res = new BusinessLayerResult<Islem>();
                res.Result = islem;


                _context.Islem.Update(res.Result);
                _context.SaveChanges();

                if (res.Errors.Count > 0) /// Hata varsa
                {
                    ErrorViewModel errorNotifyObj = new ErrorViewModel()
                    {
                        Items = res.Errors,
                        Title = "İşlem Güncellenemedi",
                        RedirectingUrl = "~/Islem/islemler"
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



    }
}

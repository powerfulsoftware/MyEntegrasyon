using MyEntegrasyon.Data.Entities;

namespace MyEntegrasyon.Models.ViewModel
{
    public class newJSONviewModel: MyEntityBase
    {
        public string? Name { get; set; } // Jsonun ne için gerreekli olduğu
        public string? Description { get; set; } // Detaylı açıklama
        public string? Pattern { get; set; } // Desen
        public string? TypeId { get; set; } // Tipi CDUR // Oluşturma - Silme - Güncelleme - Okuma
        public string? FirmaId { get; set; }

        public List<string>? Tags { get; set; }

    }


}

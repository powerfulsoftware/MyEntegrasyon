using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MyEntegrasyon.Data.Entities
{
    public class JsonDesen:MyEntityBase
    {
        [DisplayName("Name"), StringLength(200, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string? Name { get; set; } // Jsonun ne için gerreekli olduğu
        public string? Description { get; set; } // Detaylı açıklama
        public string? Pattern { get; set; } // Desen
        [DisplayName("Type"), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string? TypeId { get; set; } // Tipi CDUR // Oluşturma - Silme - Güncelleme - Okuma
        [DisplayName("Firma"), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string? FirmaId { get; set; }

        virtual public string FullName
        {
            get { return string.Format("{0} ({1} - {2} - {3})", Name, Description, FirmaId, TypeId); }
        }



    }


}

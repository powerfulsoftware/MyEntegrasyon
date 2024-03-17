using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MyEntegrasyon.Data.Entities
{
    public class Islem : MyEntityBase
    {
        [DisplayName("İşlem Adı"), StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string? IslemAdi { get; set; }


        [DisplayName("Açıklama")]
        public string? Aciklama { get; set; }

        [DisplayName("Json Desen")]
        public int JsonDesenId { get; set; }
        virtual public JsonDesen? JsonDesen { get; set; }


       

    }
}

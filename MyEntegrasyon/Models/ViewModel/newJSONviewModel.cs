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

        public class Typeliste
        {
            public string? Id { get; set; }
            public string? deger { get; set; }
        }
        public class Firmaliste
        {
            public string? Id { get; set; }
            public string? deger { get; set; }
        }

        public static List<Typeliste> typelistes()
        {
             List<Typeliste> _type = new List<Typeliste>();
            _type.Add(new Typeliste { Id = "Create", deger = "Create" });
            _type.Add(new Typeliste { Id = "Read", deger = "Read" });
            _type.Add(new Typeliste { Id = "Update", deger = "Update" });
            _type.Add(new Typeliste { Id = "Delete", deger = "Delete" });

            return _type;
        }


        public static List<Firmaliste> firmalistes()
        {
            List<Firmaliste> _firma = new List<Firmaliste>();
            _firma.Add(new Firmaliste { Id = "Ikas", deger = "Ikas" });
            _firma.Add(new Firmaliste { Id = "Nebim", deger = "Nebim" });

            return _firma;
        }


    }


}

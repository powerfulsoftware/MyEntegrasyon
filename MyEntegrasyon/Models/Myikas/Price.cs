namespace MyEntegrasyon.Models.Myikas
{
    public class Price
    {
        public float buyPrice { get; set; }  // alış fiyatı
        public string? currency { get; set; } // para birimi
        public float? discountPrice { get; set; } // İndirimfiyat
        public float sellPrice { get; set; } // satış fiyatı
        
    }
}

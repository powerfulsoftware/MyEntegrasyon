using Newtonsoft.Json;

namespace MyEntegrasyon.Models.Nebim
{
    public class ProductVariant
    {

        public string?  ProductID { get; set; }
        virtual public Product? Product { get; set; }

        public string? CurrencyCode { get; set; }
        public string? Barcode { get; set; }
        public string? GenderCode { get; set; }
        public string? ColorCode { get; set; }
        public string? ColorDesc { get; set; }
        public int ItemDimTypeCode { get; set; }
        public string? ItemDim1Code { get; set; }
        public string? ItemDim1Desc { get; set; }
        public string? ItemDim2Code { get; set; }
        public string? ItemDim2Desc { get; set; }
        public string? ItemDim3Code { get; set; }
        public string? ItemDim3Desc { get; set; }
       
        public int Qty { get; set; }
        public int Vat { get; set; }
        public double Price1 { get; set; }
        public double Price2 { get; set; }
        public string? Price3 { get; set; }
        // public int Price3 { get; set; }
        public double Price4 { get; set; }
        public double Price5 { get; set; }
        //public int AlisFiyati { get; set;

        public string? AlisFiyati { get; set; }
        public string? ProductAtt10 { get; set; }
        public string? ProductAtt10Desc { get; set; }
        public string? PAZARYERIISK { get; set; }
        public double N11_LST { get; set; }
        public double N11_IND { get; set; }
        public double AMAZON_LST { get; set; }
        public double AMAZON_IND { get; set; }
        public double CICEK_LST { get; set; }
        public double CICEK_IND { get; set; }
        public double GITTIGIDIYOR_LST { get; set; }
        public double GITTIGIDIYOR_IND { get; set; }
        public double HEPSIBURADA_LST { get; set; }
        public double HEPSIBURADA_IND { get; set; }
        public double MORHIPO_LST { get; set; }
        public double MORHIPO_IND { get; set; }
        public double PAZARAMA_LST { get; set; }
        public double PAZARAMA_IND { get; set; }
        public double TRENDYOL_LST { get; set; }
        public double TRENDYOL_IND { get; set; }
        public double BISIFIRAT_LST { get; set; }
        public double BISIFIRAT_IND { get; set; }

        [JsonProperty("TT-TURK_LST")]
        public double TTTURK_LST { get; set; }

        [JsonProperty("TT-TURK_IND")]
        public double TTTURK_IND { get; set; }
        public double BREND_LST { get; set; }
        public double BREND_IND { get; set; }
        public string? Image1 { get; set; }
        public string? Image2 { get; set; }
        public string? Image3 { get; set; }
        public string? Image4 { get; set; }
        public string? Image5 { get; set; }
        public string? Image6 { get; set; }
        public string? Image7 { get; set; }
        public string? Image8 { get; set; }



    }
}

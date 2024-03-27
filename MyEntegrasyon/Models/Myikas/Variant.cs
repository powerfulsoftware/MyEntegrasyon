namespace MyEntegrasyon.Models.Myikas
{
    public class Variant
    {
        public bool isActive { get; set; }
        // public string[]? barcodeList { get; set; }
        public List<Price>? prices { get; set; }
       // public string? SKU { get; set; }

       public List<VariantValue>? variantValueIds { get; set; }

    }
}

namespace MyEntegrasyon.Models.Myikas.SaveProduct
{
    public class Variant
    {
        public bool deleted { get; set; }
        public string? id { get; set; }
        public bool isActive { get; set; }
        public string? sku { get; set; }
        public List<Price>? prices { get; set; }
        public List<VariantValueId>? variantValueIds { get; set; }
    }
}

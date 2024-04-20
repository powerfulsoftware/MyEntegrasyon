namespace MyEntegrasyon.Models.Myikas.ListProduct
{
    public class Variant
    {
        public bool deleted { get; set; }
        public string id { get; set; }
        public bool isActive { get; set; }
        public List<Price>? prices { get; set; }

        public List<variantValueIds>? variantValueIds { get; set; }
        
    }
}

using Newtonsoft.Json;

namespace MyEntegrasyon.Models.Nebim
{
    public class Product
    {

        public string? Id { get; set; }

        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string? ItemDesc { get; set; }
      
        public string? Cat01Code { get; set; }
        public string? Cat01Desc { get; set; }
        public string? Cat02Code { get; set; }
        public string? Cat02Desc { get; set; }
        public string? Cat03Code { get; set; }
        public string? Cat03Desc { get; set; }
        public string? Cat04Code { get; set; }
        public string? Cat04Desc { get; set; }
        public string? Cat05Code { get; set; }
        public string? Cat05Desc { get; set; }
        public string? Cat06Code { get; set; }
        public string? Cat06Desc { get; set; }
        public string? Cat07Code { get; set; }
        public string? Cat07Desc { get; set; }
        public string? BrandCode { get; set; }
        public string? BrandDesc { get; set; }


        public List<ProductVariant>? ProductVariants { get; set; }
        
    }
}

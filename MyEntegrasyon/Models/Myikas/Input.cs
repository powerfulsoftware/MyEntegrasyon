namespace MyEntegrasyon.Models.Myikas
{
    public class Input
    {
      //  public string? Id { get; set; }  

       // public string? brand { get; set; }
        public string? brandId { get; set; }

       // public categories? categoryIds { get; set; }
        public List<string>? categoryIds { get; set; }

        public string? name { get; set; }

        public string? id { get; set; }

        public string? shortDescription { get; set; }

       // public float totalStock { get; set; }
        public string? type { get; set; }
        public List<Variant>? variants { get; set; }
       public List<SalesChannel>? salesChannels { get; set; }
        public string? salesChannelIds { get; set; }

        public List<ProductVariantType>? productVariantTypes { get; set; }
    }
}

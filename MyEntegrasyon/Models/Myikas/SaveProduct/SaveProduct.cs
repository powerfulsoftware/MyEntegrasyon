namespace MyEntegrasyon.Models.Myikas.SaveProduct
{
    public class SaveProduct
    {
        public string? brandId { get; set; }
        public List<string>? categoryIds { get; set; }
        public string? id { get; set; }
        public string? name { get; set; }
        public string? shortDescription { get; set; }
        public string? type { get; set; }
        public List<Variant>? variants { get; set; }
        public List<ProductVariantType>? productVariantTypes { get; set; }
        public List<string>? salesChannelIds { get; set; }
        public List<SalesChannel>? salesChannels { get; set; }
    }
}

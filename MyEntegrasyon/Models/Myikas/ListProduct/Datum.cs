namespace MyEntegrasyon.Models.Myikas.ListProduct
{
    public class Datum
    {
        public bool deleted { get; set; }
        public string? id { get; set; }
        public string? name { get; set; }
        public string? shortDescription { get; set; }
        public string? type { get; set; }
        public List<Variant>? variants { get; set; }
    }
}

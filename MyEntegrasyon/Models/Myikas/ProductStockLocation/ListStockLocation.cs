namespace MyEntegrasyon.Models.Myikas.ProductStockLocation
{
    public class ListStockLocation
    {
        public bool deleted { get; set; }
        public string? id { get; set; }
        public string? name { get; set; }
        public List<string>? outOfStockMailList { get; set; }
    }
}

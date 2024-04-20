namespace MyEntegrasyon.Models.Myikas.ListProduct
{
    public class ListProduct
    {
        public int count { get; set; }
        public List<Datum>? data { get; set; }
        public bool hasNext { get; set; }
        public int limit { get; set; }
        public int page { get; set; }
    }
}

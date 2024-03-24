using Newtonsoft.Json.Linq;

namespace MyEntegrasyon.Models.Myikas.VariantC
{
    public class ListVariantType
    {
        public bool deleted { get; set; }
        public string? id { get; set; }
        public string? name { get; set; }
        public string? selectionType { get; set; }
        public List<Value>? values { get; set; }
    }
}

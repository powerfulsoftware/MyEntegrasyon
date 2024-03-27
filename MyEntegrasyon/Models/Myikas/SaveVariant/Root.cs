namespace MyEntegrasyon.Models.Myikas.SaveVariant
{
    public class Root
    {
        public Input? input { get; set; }
        public SaveVariantType? saveVariantType { get; set; }

        public List<string>? idList { get; set; }

        public bool deleteVariantTypeList { get; set; } // variyant listesi silinince dönen değer
    }
}

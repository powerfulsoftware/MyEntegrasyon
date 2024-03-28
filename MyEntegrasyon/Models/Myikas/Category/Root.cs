namespace MyEntegrasyon.Models.Myikas.Category
{
    public class Root
    {
        public Input? input { get; set; }
        public List<ListCategory>? listCategory { get; set; }
        public SaveCategory? saveCategory { get; set; }

        public List<string>? idList { get; set; }  // category listesi silinecek IDler

        public bool deleteCategoryList { get; set; } // category listesi silinince dönen değer

    }
}

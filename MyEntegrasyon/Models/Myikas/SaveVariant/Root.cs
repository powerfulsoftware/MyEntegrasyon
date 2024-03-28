﻿namespace MyEntegrasyon.Models.Myikas.SaveVariant
{
    public class Root
    {
        public Input? input { get; set; }
        public SaveVariantType? saveVariantType { get; set; }

        public List<string>? idList { get; set; }

        public bool deleteVariantTypeList { get; set; } // variyant listesi silinince dönen değer

        public Name? name { get; set; } // variyant listesi name ile çekme 

        public List<ListVariantType>? listVariantType { get; set; }  // variyant listesi name ile çekme  // sonuc bu kısma aktarılacak
    }
}
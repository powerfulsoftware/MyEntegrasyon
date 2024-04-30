namespace MyEntegrasyon.Data.Entities
{
    public class VariantValue: MyEntityBase
    {
        
        public int VariantId { get; set; }
        virtual public Variant? Variant { get; set; }
        public string? IkasId { get; set; }
        public string? name { get; set; }
        public string? colorCode { get; set; }


    }
}

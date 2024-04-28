namespace MyEntegrasyon.Data.Entities
{
    public class Variant : MyEntityBase
    {
        public string? IkasId { get; set; }
        public string? name { get; set; }
        public string? selectionType { get; set; }
        virtual public List<VariantValue>? values { get; set; }
    }
}

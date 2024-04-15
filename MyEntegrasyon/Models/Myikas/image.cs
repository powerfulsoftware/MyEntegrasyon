namespace MyEntegrasyon.Models.Myikas
{
#pragma warning disable CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.
    public class image
#pragma warning restore CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.
    {
        public string? fileName { get; set; }

        public string? imageId { get; set; }
        public bool isMain { get; set; }
        public float order { get; set; }
    }
}

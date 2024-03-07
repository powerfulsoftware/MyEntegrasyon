namespace MyEntegrasyon.Models.Myikas
{
    public class Input
    {
        public string? name { get; set; }
        public string? type { get; set; }
        public List<Variant>? variants { get; set; }
        public List<SalesChannel>? salesChannels { get; set; }
        public string? salesChannelIds { get; set; }
    }
}

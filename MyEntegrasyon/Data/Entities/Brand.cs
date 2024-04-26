using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEntegrasyon.Data.Entities
{
    public class Brand
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? BrandCode { get; set; }
        public string? BrandDesc { get; set; }
        public string? ikasId { get; set; }
    }
}

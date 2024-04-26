using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEntegrasyon.Data.Entities
{
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Cat01Code { get; set; }
        public string? Cat01Desc { get; set; }
        public string? ikasId { get; set; }
    }
}

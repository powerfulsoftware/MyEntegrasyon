using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MyEntegrasyon.Data.Entities
{
    public class AppRole : IdentityRole<int>
    {
        [StringLength(128)]
        public string? Description { get; set; }
    }
}

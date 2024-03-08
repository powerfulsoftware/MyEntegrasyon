using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MyEntegrasyon.Data.Entities
{
    public class AppUser : IdentityUser<int>
    {
        [DisplayName("TC"), StringLength(11, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string? TC { get; set; }

        [DisplayName("Adı"), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string? Adi { get; set; }

        [DisplayName("Soyadı"), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string? Soyadi { get; set; }

        [DisplayName("Mail"), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string? Mail { get; set; }

        [DisplayName("Fotoğraf"), StringLength(20, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string? Fotograf { get; set; }

        [DisplayName("Atiflik Durumu")]
        public bool IsActive { get; set; }


    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MyEntegrasyon.Models.ViewModel
{
    public class ViewModelLogin
    {

        [EmailAddress]
        [DisplayName("Email"),
            Required(ErrorMessage = "{0}, alanı boş geçilemez."),
            StringLength(100, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string? Email { get; set; }

        [DisplayName("Şifre"),
          Required(ErrorMessage = "{0}, alanı boş geçilemez."),
          StringLength(100, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        //[Display(Name = "Remember me?")]
        //public bool RememberMe { get; set; }
    }
}

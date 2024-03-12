using System.ComponentModel;

namespace MyEntegrasyon.Models.ViewModel
{
    public class UserRolAtamaViewModel
    {
        public string? Id { get; set; }

        [DisplayName("Adı Soyadı")]
        public string? UserName { get; set; }

        public string? Email { get; set; }

        public RoleViewModel[]? Roles { get; set; }
    }
}

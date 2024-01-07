using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MicroserviceEnlistment.Models
{
    public class User : IdentityUser
    {
        [Display(Name = "Nombres")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string LastName { get; set; }

        [Display(Name = "Dirección")]
        [MaxLength(200, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string Address { get; set; }

        public string TypeOfDocument { get; set; }

        public string Document { get; set; }

        [Display(Name = "Usuario")]
        public string FullName => $"{FirstName} {LastName}";
    }
}

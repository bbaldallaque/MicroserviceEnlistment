using System.ComponentModel.DataAnnotations;

namespace MicroserviceEnlistment.ViewModels
{
    public class RecoverPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

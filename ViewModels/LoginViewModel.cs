using System.ComponentModel.DataAnnotations;

namespace MicroserviceEnlistment.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MicroserviceEnlistment.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "The {0} fiel must contain between {2} and {1} characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "The {0} fiel must contain between {2} and {1} characters")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirPassword { get; set; }

        [Required]
        public string Token { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MicroserviceEnlistment.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Display(Name = "Current Password")]
        [Required(ErrorMessage = "The fiel {0} is mandatory.")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "The {0} fiel must contain between {2} and {1} characters")]
        public string OldPassword { get; set; }

        [Display(Name = "New Password")]
        [Required(ErrorMessage = "The fiel {0} is mandatory.")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "The {0} fiel must contain between {2} and {1} characters")]
        public string NewPassword { get; set; }

        [Display(Name = "Password Confirm")]
        [Required(ErrorMessage = "The fiel {0} is mandatory.")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "The {0} fiel must contain between {2} and {1} characters")]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}

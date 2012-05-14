using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ZenMu.Validations;
using DataAnnotationsExtensions;

namespace ZenMu.Models
{
    public class NewUserViewModel
    {
        [UsernameAvailable]
        [Required(ErrorMessage = "This field is required.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Username must be between 2 and 60 characters long")]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Username must only contain alphanumeric characters and underscores.")]
        public string Username { get; set; }
        
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This field is required.")]
        [Compare("RepeatPassword", ErrorMessage = "Passwords do not match.")]
        [StringLength(100, MinimumLength = 12, ErrorMessage = "Passwords must be at least 12 characters long")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Repeat Password")]
        [Required(ErrorMessage = "This field is required.")]
        [StringLength(100, MinimumLength = 12, ErrorMessage = "Passwords must be at least 12 characters long")]
        public string RepeatPassword { get; set; }

        [Email]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
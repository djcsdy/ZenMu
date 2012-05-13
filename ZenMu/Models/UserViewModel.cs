using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZenMu.Models
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "This field is required.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Username must be between 2 and 60 characters long")]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Username must only contain alphanumeric characters and underscores.")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "This field is required.")]
        [Compare("RepeatPassword", ErrorMessage = "Passwords do not match.")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "This field is required.")]
        public string RepeatPassword { get; set; }

        public string Email { get; set; }
        public string[] Roles { get; private set; }
        public bool IsDeleted { get; private set; }
    }
}
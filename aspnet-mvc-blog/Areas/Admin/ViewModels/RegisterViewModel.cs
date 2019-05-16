using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace aspnet_mvc_blog.Areas.Admin.ViewModels
{
    public class RegisterViewModel : LoginViewModel
    {
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, try again !")]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}
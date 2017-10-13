using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Querier.Models.Login
{
    public class UserRegistration
    {
        [Required]
        [Display(Name = "Username")]
        public string username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [CompareAttribute("password", ErrorMessage = "Your passwords do not match.")]
        public string confirmPassword { get; set; }
    }
}

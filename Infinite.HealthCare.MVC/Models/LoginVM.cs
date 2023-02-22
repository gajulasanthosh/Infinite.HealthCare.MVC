using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.HealthCare.MVC.Models
{
    public class LoginVM
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class RegisterVM
    {
        
        public int Id { get; set; }
        [Required(ErrorMessage ="Username cannot be empty")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Password cannot be empty")]
        [StringLength(20, MinimumLength =6,ErrorMessage ="Password should be 6 to 20 characters")]
        public string Password { get; set; }
        [Required(ErrorMessage ="Confirm password could not be empty")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match")]
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
    }

    public class RolesRegisterVM
    {
        public string SelectedValue { get; set; }
        public IEnumerable<SelectListItem> Values { get; set; }
        public RegisterVM RegisterRoles { get; set; }

    }
}

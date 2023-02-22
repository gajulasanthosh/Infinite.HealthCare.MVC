using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.HealthCare.MVC.Models
{
    public class PatientVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Please provide FullName")]
        [Display(Name ="Full Name")]
        [StringLength(40, MinimumLength =4)]
        public string FullName { get; set; }
        [Required(ErrorMessage ="Please select your Gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage ="Please enter your Age")]
        public int Age { get; set; }
        [Required(ErrorMessage ="Please enter your Email Address")]
        [EmailAddress(ErrorMessage ="Please enter valid Email Address")]
        public string EmailId { get; set; }

        [Required(ErrorMessage ="Please enter address")]
        public string Address { get; set; }
        [Required(ErrorMessage ="Please enter City")]
        public string City { get; set; }
        [Required(ErrorMessage ="Please enter State")]
        public string State { get; set; }
        [Required(ErrorMessage ="Please enter PinCode")]
        [StringLength(6,MinimumLength =6,ErrorMessage ="Please enter valid pincode")]
        public string PinCode { get; set; }

        [Required(ErrorMessage ="Please enter your Mobile Number")]
        [Phone]
        [RegularExpression("^([6-9][0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        [Display(Name ="Mobile Number")]
        [StringLength(10,MinimumLength =10,ErrorMessage ="Please enter valid Mobile Number")]
        public string MobileNo { get; set; }
        public int UserId { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.HealthCare.MVC.Models
{
    public class DoctorVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter Doctor Name")]
        [Display(Name ="Doctor Name")]
        [StringLength(40,MinimumLength =4,ErrorMessage ="Name Should be atleast 4 characters")]
        public string DoctorName { get; set; }
        [Required(ErrorMessage ="Please enter Email Id")]
        [EmailAddress(ErrorMessage ="Please enter valid Email Address")]
        public string EmailId { get; set; }
        
        [Required(ErrorMessage ="Please enter Qualification")]
        public string Qualification { get; set; }
        [Required(ErrorMessage ="Please select Specialization")]
        public string Specilization { get; set; }
        [Required(ErrorMessage = "Please enter address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter City")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please enter State")]
        public string State { get; set; }
        [Required(ErrorMessage = "Please enter your Mobile Number")]
        [Phone]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Please enter valid Mobile Number")]
        public string PhoneNo { get; set; }
        //public int? UserId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.HealthCare.MVC.Models
{
    public class AppointmentVM
    {
        public int Id { get; set; }
        
        public int PatientId { get; set; }
        [Required(ErrorMessage = "Please select a doctor")]
        [Display(Name ="Doctor")]

        public int DoctorId { get; set; }
        [Required(ErrorMessage ="Please provide Appointment Date")]
        [Display(Name ="Appointment Date")]
        public DateTime AppointmentDate { get; set; }
        [Required(ErrorMessage ="Please provide Appointment Time")]
        [Display(Name ="Appointment Time")]
        public DateTime AppointmentTime { get; set; }
        [Required(ErrorMessage ="Please explain the problem")]
        [StringLength(150)]
        public string Problem { get; set; }

        public string PatientName { get; set; }
        public string DoctorName { get; set; }

    }
}

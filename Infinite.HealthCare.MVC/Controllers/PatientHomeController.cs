using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.HealthCare.MVC.Controllers
{
    public class PatientHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

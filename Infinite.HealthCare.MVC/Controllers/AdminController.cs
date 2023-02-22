using Infinite.HealthCare.MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infinite.HealthCare.MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IConfiguration _Configuration;

        public AdminController(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> DoctorList()
        {
            List<DoctorVM> doctors = new();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                client.BaseAddress = new System.Uri(_Configuration["ApiUrl:api"]);
                var result = await client.GetAsync("Doctor/GetAllDoctors");
                if (result.IsSuccessStatusCode)
                {
                    doctors = await result.Content.ReadAsAsync<List<DoctorVM>>();
                }
            }
            return View(doctors);
        }

        public async Task<IActionResult> DoctorDetails(int id)
        {
            DoctorVM doctor = null;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                client.BaseAddress = new System.Uri(_Configuration["ApiUrl:api"]);
                var result = await client.GetAsync($"Doctor/GetDoctorById/{id}");
                if (result.IsSuccessStatusCode)
                {
                    doctor = await result.Content.ReadAsAsync<DoctorVM>();
                }
            }
            return View(doctor);
        }

        public async Task<IActionResult> PatientList()
        {
            List<PatientVM> patients = new();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                client.BaseAddress = new System.Uri(_Configuration["ApiUrl:api"]);
                var result = await client.GetAsync("Patient/GetAllPatients");
                if (result.IsSuccessStatusCode)
                {
                    patients = await result.Content.ReadAsAsync<List<PatientVM>>();
                }
            }
            return View(patients);
        }

        public async Task<IActionResult> PatientDetails(int id)
        {
            PatientVM patient = null;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                client.BaseAddress = new System.Uri(_Configuration["ApiUrl:api"]);
                var result = await client.GetAsync($"Patient/GetPatientById/{id}");
                if (result.IsSuccessStatusCode)
                {
                    patient = await result.Content.ReadAsAsync<PatientVM>();
                }
            }
            return View(patient);
        }
    }
}

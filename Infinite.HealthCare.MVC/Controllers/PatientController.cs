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
    public class PatientController : Controller
    {
        private readonly IConfiguration _Configuration;

        public PatientController(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            List<PatientVM> patients = new();
            using (var client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                client.BaseAddress = new System.Uri(_Configuration["ApiUrl:api"]);
                var result = await client.GetAsync("Patient/GetAllPatients");
                if (result.IsSuccessStatusCode)
                {
                    patients = await result.Content.ReadAsAsync<List<PatientVM>>();
                }
            }
            return View(patients);
        }

        public async Task<IActionResult> Details(int id)
        {
            PatientVM patient = null;
            using (var client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                client.BaseAddress = new System.Uri(_Configuration["ApiUrl:api"]);
                var result = await client.GetAsync($"Patient/GetPatientById/{id}");
                if (result.IsSuccessStatusCode)
                {
                    patient = await result.Content.ReadAsAsync<PatientVM>();
                }
            }
            return View(patient);
        }

        [HttpGet]
        public  IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PatientVM patient)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                    client.BaseAddress = new Uri(_Configuration["ApiUrl:api"]);
                    var result = await client.PostAsJsonAsync("Patient/CreatePatient", patient);
                    if (result.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        return RedirectToAction("Index", "Patient");
                    }
                }
            }
            
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (ModelState.IsValid)
            {
                PatientVM patient = null;
                using (var client = new HttpClient())
                {
                    //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                    client.BaseAddress = new Uri(_Configuration["ApiUrl:api"]);
                    var result = await client.GetAsync($"Patient/GetPatientById/{id}");
                    if (result.IsSuccessStatusCode)
                    {
                        patient = await result.Content.ReadAsAsync<PatientVM>();
                        return View(patient);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Patient doesn't exist");
                    }

                }
            }
            return View();
        }

        [HttpPost]
        [Route("Patient/Edit/{Id}")]
        public async Task<IActionResult> Edit(PatientVM patient)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_Configuration["ApiUrl:api"]);
                    var result = await client.PutAsJsonAsync($"Patient/UpdatePatient/{patient.Id}", patient);
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Server Error, Please try later");
                    }

                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            PatientVM patient = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(_Configuration["ApiUrl:api"]);
                var result = await client.GetAsync($"Patient/GetPatientById/{id}");
                if (result.IsSuccessStatusCode)
                {
                    patient = await result.Content.ReadAsAsync<PatientVM>();
                    return View(patient);

                }
                else
                {
                    ModelState.AddModelError("", "Server Error.Please try later");
                }
            }
            return View(patient);
        }



        [HttpPost("Patient/Delete/{Id}")]
        public async Task<IActionResult> Delete(PatientVM patient)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(_Configuration["ApiUrl:api"]);
                var result = await client.DeleteAsync($"Patient/DeletePatient/{patient.Id}");
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");

                }
                else
                {
                    ModelState.AddModelError("", "Server Error.Please try later");
                }
            }
            return View();

        }
    }
}

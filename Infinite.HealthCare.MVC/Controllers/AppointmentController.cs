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
    public class AppointmentController : Controller
    {
        private readonly IConfiguration _Configuration;

        public AppointmentController(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            List<AppointmentVM> appointments = new();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                client.BaseAddress = new System.Uri(_Configuration["ApiUrl:api"]);
                var result = await client.GetAsync("Appointment/GetAllAppointments");
                if (result.IsSuccessStatusCode)
                {
                    appointments = await result.Content.ReadAsAsync<List<AppointmentVM>>();
                }
            }
            return View(appointments);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppointmentVM appointment)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                    client.BaseAddress = new Uri(_Configuration["ApiUrl:api"]);
                    var result = await client.PostAsJsonAsync("Appointment/CreateAppointment", appointment);
                    if (result.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        return RedirectToAction("Index", "PatientHome");
                    }
                }
            }

            return View();

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            AppointmentVM appointment = null;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                client.BaseAddress = new System.Uri(_Configuration["ApiUrl:api"]);
                var result = await client.GetAsync($"Appointent/GetAppointmentById/{id}");
                if (result.IsSuccessStatusCode)
                {
                    appointment = await result.Content.ReadAsAsync<AppointmentVM>();
                    return View(appointment);

                }
                else
                {
                    ModelState.AddModelError("", "Server Error.Please try later");
                }
            }
            return View(appointment);
        }



        [HttpPost("Appointment/Delete/{Id}")]
        public async Task<IActionResult> Delete(AppointmentVM appointment)
        {

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                client.BaseAddress = new System.Uri(_Configuration["ApiUrl:api"]);
                var result = await client.DeleteAsync($"Appointment/DeleteAppointment/{appointment.Id}");
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

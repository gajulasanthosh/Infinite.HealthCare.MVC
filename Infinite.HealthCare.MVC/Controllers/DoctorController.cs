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
    public class DoctorController : Controller
    {
        private readonly IConfiguration _Configuration;

        public DoctorController(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
        public async Task<IActionResult> Index()
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

        public async Task<IActionResult> Details(int id)
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

        [HttpGet]
        public IActionResult Create()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                return View();
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> Create(DoctorVM doctor)
        {
            if (!ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                    client.BaseAddress = new Uri(_Configuration["ApiUrl:api"]);
                    var userId = HttpContext.Session.GetString("UserId");
                    doctor.UserId = int.Parse(userId);
                    var result = await client.PostAsJsonAsync("Doctor/CreateDoctor", doctor);
                    
                    if (result.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        return RedirectToAction("Index", "DoctorHome");
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
                DoctorVM doctor = null;
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                    client.BaseAddress = new Uri(_Configuration["ApiUrl:api"]);
                    var result = await client.GetAsync($"Doctor/GetDoctorById/{id}");
                    if (result.IsSuccessStatusCode)
                    {
                        doctor = await result.Content.ReadAsAsync<DoctorVM>();
                        return View(doctor);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Doctor doesn't exist");
                    }

                }
            }
            return View();
        }

        //[HttpGet]
        //public async Task<IActionResult> Edit2(int id)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        DoctorVM doctor = null;
        //        using (var client = new HttpClient())
        //        {
        //            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
        //            client.BaseAddress = new Uri(_Configuration["ApiUrl:api"]);
        //            //var userId = HttpContext.Session.GetString("UserId");
        //            //var ID = int.Parse(userId);
        //            var doctorID = await client.GetAsync("Accounts/GetIdforEdit");
        //            if (doctorID.IsSuccessStatusCode)
        //            {
        //                var id2 = await doctorID.Content.ReadAsAsync<>();
        //                id2 = id;
        //            }
                    
        //            var result = await client.GetAsync($"Doctor/GetDoctorById/{id}");
        //            if (result.IsSuccessStatusCode)
        //            {
        //                doctor = await result.Content.ReadAsAsync<DoctorVM>();
        //                return View(doctor);
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", "Doctor doesn't exist");
        //            }

        //        }
        //    }
        //    return View();
        //}

        [HttpPost]
        [Route("Doctor/Edit/{Id}")]
        public async Task<IActionResult> Edit(DoctorVM doctor)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                    client.BaseAddress = new Uri(_Configuration["ApiUrl:api"]);
                    var result = await client.PutAsJsonAsync($"Doctor/UpdateDoctor/{doctor.Id}", doctor);
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("DoctorList","Admin");
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
            DoctorVM doctor = null;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                client.BaseAddress = new System.Uri(_Configuration["ApiUrl:api"]);
                var result = await client.GetAsync($"Doctor/GetDoctorById/{id}");
                if (result.IsSuccessStatusCode)
                {
                    doctor = await result.Content.ReadAsAsync<DoctorVM>();
                    return View(doctor);

                }
                else
                {
                    ModelState.AddModelError("", "Server Error.Please try later");
                }
            }
            return View(doctor);
        }



        [HttpPost("Doctor/Delete/{Id}")]
        public async Task<IActionResult> Delete(DoctorVM doctor)
        {

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                client.BaseAddress = new System.Uri(_Configuration["ApiUrl:api"]);
                var result = await client.DeleteAsync($"Doctor/DeleteDoctor/{doctor.Id}");
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("DoctorList","Admin");

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

using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using System;
using System.Collections.Generic;

namespace HospitalManagement.Controllers
{
    public class AppointmentController : Controller
    {
        readonly IAppointmentBusiness appointmentBusiness;

        public AppointmentController(IAppointmentBusiness appointmentBusiness)
        {
            this.appointmentBusiness = appointmentBusiness;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AppointmentModel appointmentModel)
        {
            
            bool isCreated = appointmentBusiness.CreateAppointment(appointmentModel);
            if (isCreated)
            {
                return RedirectToAction("Create");
            }
            return View("Index");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var appointments = appointmentBusiness.GetAll();
            return View(appointments);
        }
    }
}

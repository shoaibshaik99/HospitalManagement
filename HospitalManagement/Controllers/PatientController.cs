using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using System.Collections.Generic;

namespace HospitalManagement.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientBusiness patientBusiness;

        public PatientController(IPatientBusiness patientBusiness)
        {
            this.patientBusiness = patientBusiness;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateProfile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProfile(PatientModel patientModel)
        {
            bool isCreated = patientBusiness.CreateProfile(patientModel);
            if (isCreated)
            {
                return RedirectToAction("CreateProfile");
            }
            return View("Index");
        }

        [HttpGet]
        public IActionResult GetAllPatients()
        {
            List<PatientModel> patients = patientBusiness.GetAllPatients();
            return View(patients);
        }

        [HttpGet]
        public IActionResult GetPatientById(int id)
        {
            PatientModel patient = patientBusiness.GetPatientById(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        [HttpGet]
        public IActionResult UpdatePatient(int id)
        {
            PatientModel patient = patientBusiness.GetPatientById(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        [HttpPost]
        public IActionResult UpdatePatient(int id, PatientModel patientModel)
        {
            if (id != patientModel.Patient_Id)
            {
                return BadRequest();
            }

            bool isUpdated = patientBusiness.UpdatePatient(patientModel);
            if (isUpdated)
            {
                return RedirectToAction("GetAllPatients");
            }
            else
            {
                return View("Index");
            }
        }

        [HttpGet]
        public IActionResult DeletePatient(int id)
        {
            PatientModel patient = patientBusiness.GetPatientById(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        [HttpPost, ActionName("DeletePatient")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            bool isDeleted = patientBusiness.DeletePatient(id);
            if (isDeleted)
            {
                return RedirectToAction("GetAllPatients");
            }
            else
            {
                return RedirectToAction("GetAllPatients");
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            LoginModel login = patientBusiness.Login(loginModel);
            if (login == null)
            {
                return NotFound();
            }
            HttpContext.Session.SetInt32("PatientId", login.LoginId);
            return RedirectToAction("GetpatientById", new { id = loginModel.LoginId});
        }
    }
}

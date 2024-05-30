using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;

namespace HospitalManagement.Controllers
{
    public class DoctorController : Controller
    {
        readonly IDoctorBusiness doctorBusiness;

        public DoctorController(IDoctorBusiness doctorBusiness)
        {
            this.doctorBusiness = doctorBusiness;
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
        public IActionResult CreateProfile(DoctorModel doctorModel)
        {
            bool isCreated = doctorBusiness.CreateProfile(doctorModel);
            if (isCreated)
            {
                return RedirectToAction("CreateProfile");
            }
            return View("Index");
        }

        [HttpGet]
        public IActionResult GetAllDoctors()
        {
            List<DoctorModel> doctors = doctorBusiness.GetAllDoctors();
            return View(doctors);
        }

        [HttpGet]
        //[Route("Doctor/GetById/{doctorId}")]
        public IActionResult GetDoctorById(int doctorId)
        {
            DoctorModel doctor = doctorBusiness.GetDoctorById(doctorId);
            if (doctor == null)
            {
                return NotFound();
            }
            HttpContext.Session.SetInt32("DoctorId",doctorId);
            return View(doctor);
        }

        [HttpGet]
        public IActionResult UpdateDoctor(int doctorId)
        {
            DoctorModel doctor = doctorBusiness.GetDoctorById(doctorId);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        [HttpPost]
        public IActionResult UpdateDoctor(int doctorId, DoctorModel doctorModel)
        {
            if (doctorId != doctorModel.DoctorId)
            {
                return BadRequest();
            }

            bool isUpdated = doctorBusiness.UpdateDoctor(doctorModel);
            if (isUpdated)
            {
                return RedirectToAction("GetAllDoctors");
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet]
        public IActionResult DeleteDoctor(int id)
        {
            DoctorModel doctor = doctorBusiness.GetDoctorById(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }


        [HttpPost, ActionName("DeleteDoctor")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            bool isDeleted = doctorBusiness.DeleteDoctor(id);
            if (isDeleted)
            {
                return RedirectToAction("GetAllDoctors");
            }
            else
            {
                
                return RedirectToAction("GetAllDoctors");
            }
        }


    }
}

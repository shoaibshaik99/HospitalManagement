using BusinessLayer.Interfaces;
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
        [Route("GetById/{doctorId}")]
        public IActionResult GetDoctorById(int doctorId)
        {
            DoctorModel doctor = doctorBusiness.GetDoctorById(doctorId);
            if (doctor == null)
            {
                return NotFound();
            }
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
                // Handle update failure
                //return RedirectToAction("GetAllDoctors");
                //return View("Index");
                return BadRequest();
            }

        }

        [HttpGet]
        public IActionResult DeleteDoctor(int doctorId)
        {
            DoctorModel doctor = doctorBusiness.GetDoctorById(doctorId);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }


        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int doctorId)
        {
            bool isDeleted = doctorBusiness.DeleteDoctor(doctorId);
            if (isDeleted)
            {
                return RedirectToAction(nameof(GetAllDoctors));
            }
            else
            {
                // Handle deletion failure+
                return RedirectToAction(nameof(GetAllDoctors));
            }
        }


    }
}

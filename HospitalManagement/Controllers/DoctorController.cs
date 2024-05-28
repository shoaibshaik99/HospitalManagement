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

    }
}

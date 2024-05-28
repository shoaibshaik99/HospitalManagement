using BusinessLayer.Interfaces;
using ModelLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class DoctorBusiness: IDoctorBusiness
    {
        readonly IDoctorRepo doctorRepo;

        public DoctorBusiness(IDoctorRepo doctorRepo)
        {
            this.doctorRepo = doctorRepo;
        }

        public bool CreateProfile(DoctorModel doctorModel)
        {
            return doctorRepo.CreateProfile(doctorModel);
        }

        public List<DoctorModel> GetAllDoctors()
        {
            return doctorRepo.GetAllDoctors();
        }
    }
}

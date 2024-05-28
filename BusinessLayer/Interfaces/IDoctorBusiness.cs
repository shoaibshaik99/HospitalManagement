using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IDoctorBusiness
    {
        public bool CreateProfile(DoctorModel doctorModel);

        public List<DoctorModel> GetAllDoctors();

        public DoctorModel GetDoctorById(int doctorId);
    }
}

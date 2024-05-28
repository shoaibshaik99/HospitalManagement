using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IDoctorRepo
    {
        public bool CreateProfile(DoctorModel doctorModel);

        public List<DoctorModel> GetAllDoctors();

        public DoctorModel GetDoctorById(int doctorId);

        public bool UpdateDoctor(DoctorModel doctorModel);

        public bool DeleteDoctor(int doctorId);
    }
}

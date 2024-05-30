using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IPatientBusiness
    {
        public bool CreateProfile(PatientModel patientModel);

        public List<PatientModel> GetAllPatients();

        public PatientModel GetPatientById(int patientId);

        public bool UpdatePatient(PatientModel patientModel);

        public bool DeletePatient(int patientId);

        public LoginModel Login(LoginModel loginModel);
    }
}

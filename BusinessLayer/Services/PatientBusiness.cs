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
    public class PatientBusiness: IPatientBusiness
    {
        readonly IPatientRepo patientRepo;

        public PatientBusiness(IPatientRepo patientRepo)
        {
            this.patientRepo = patientRepo;
        }

        public bool CreateProfile(PatientModel patientModel)
        {
            return patientRepo.CreateProfile(patientModel);
        }

        public List<PatientModel> GetAllPatients()
        {
            return patientRepo.GetAllPatients();
        }

        public PatientModel GetPatientById(int patientId)
        {
            return patientRepo.GetPatientById(patientId);
        }

        public bool UpdatePatient(PatientModel patientModel)
        {
            return patientRepo.UpdatePatient(patientModel);
        }

        public bool DeletePatient(int patientId)
        {
            return patientRepo.DeletePatient(patientId);
        }
    }
}

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
    public class AppointmentBusiness:IAppointmentBusiness
    {
        private readonly IAppointmentRepo appointmentRepo;

        public AppointmentBusiness(IAppointmentRepo appointmentRepo)
        {
            this.appointmentRepo = appointmentRepo;
        }
        public bool CreateAppointment(AppointmentModel appointmentModel)
        {
            return appointmentRepo.CreateAppointment(appointmentModel);
        }

        public List<AppointmentModel> GetAll()
        {
            return appointmentRepo.GetAll();
        }

    }
}

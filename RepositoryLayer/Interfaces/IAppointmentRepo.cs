using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IAppointmentRepo
    {
        public bool CreateAppointment(AppointmentModel appointmentModel);

        public List<AppointmentModel> GetAll();
    }
}

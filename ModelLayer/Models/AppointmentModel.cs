using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public class AppointmentModel
    {
        public int Appointment_Id { get; set; }
        public int Patient_Id { get; set; }
        public int Doctor_Id { get; set; }
        public DateTime Appointment_Date { get; set; }
        public TimeSpan Start_Time { get; set; }
        public TimeSpan End_Time { get; set; }
        public string Concerns { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public class GetMyPatientDetails
    {
        public int DoctorID { get; set; }
        public string DoctorName { get; set; }
        public string DoctorImage { get; set; }
        public int PatientID { get; set; }
        public string PatientName { get; set; }
        public string PatientImage { get; set; }
        public string PatientGender { get; set; }
        public int PatientAge { get; set; }
        public string Concerns { get; set; }
    }
}
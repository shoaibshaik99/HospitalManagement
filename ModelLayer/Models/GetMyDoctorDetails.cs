using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public class GetMyDoctorDetails
    {
        public int PatientID { get; set; }
        public string PatientName { get; set; }
        public int DoctorID { get; set; }
        public string DoctorName { get; set; }
        public string DoctorImage { get; set; }
        public int DoctorAge { get; set; }
        public string DoctorGender { get; set; }
        public string Qualification { get; set; }
        public string Specialization { get; set; }
        public int YearsOfExperience { get; set; }

    }
}

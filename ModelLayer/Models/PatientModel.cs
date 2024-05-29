using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public class PatientModel
    {
        public int Patient_Id { get; set; }
        public bool IsTrash { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string ImageLink { get; set; }
        public DateTime DOB { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Contact_Address { get; set; }
        public string Concern { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using ModelLayer.Models;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Services
{
    public class DoctorRepo: IDoctorRepo
    {
        readonly IConfiguration config;
        readonly string connectionString;
        readonly SqlConnection connection = new SqlConnection();
        
        public DoctorRepo(IConfiguration configuration)
        {
            this.config = configuration;
            connectionString = config.GetConnectionString("HospitalDBConnection");
            connection.ConnectionString = connectionString;
        }

        public bool CreateProfile(DoctorModel doctorModel)
        {            
            if (doctorModel != null && connection !=null)
            {
                connection.Open();
                SqlCommand createCommand = new SqlCommand("sp_CreateDoctorProfile", connection);
                createCommand.CommandType = CommandType.StoredProcedure;

                createCommand.Parameters.AddWithValue("@FirstName", doctorModel.FirstName);
                createCommand.Parameters.AddWithValue("@LastName", doctorModel.LastName);
                createCommand.Parameters.AddWithValue("@ContactNumber", doctorModel.ContactNumber);
                createCommand.Parameters.AddWithValue("@Email", doctorModel.Email);
                createCommand.Parameters.AddWithValue("@ImageLink", doctorModel.ImageLink);
                createCommand.Parameters.AddWithValue("@DOB", doctorModel.DOB);
                createCommand.Parameters.AddWithValue("@Gender", doctorModel.Gender);
                createCommand.Parameters.AddWithValue("@Contact_Address", doctorModel.ContactAddress);
                createCommand.Parameters.AddWithValue("@Qualification", doctorModel.Qualification);
                createCommand.Parameters.AddWithValue("@Specialization", doctorModel.Specialization);
                createCommand.Parameters.AddWithValue("@Years_Of_Experience", doctorModel.YearsOfExperience);

                createCommand.ExecuteNonQuery();
                return true;
            }
            connection.Close();
            return false;
        }

    }
}

using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ModelLayer.Models;
using RepositoryLayer.Interfaces;
using System.Dynamic;

namespace RepositoryLayer.Services
{
    public class PatientRepo : IPatientRepo
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public PatientRepo(IConfiguration configuration)
        {
            _config = configuration;
            _connectionString = _config.GetConnectionString("HospitalDBConnection");
        }

        public bool CreateProfile(PatientModel patientModel)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand createCommand = new SqlCommand("usp_CreatePatientProfile", connection);
                createCommand.CommandType = CommandType.StoredProcedure;

                createCommand.Parameters.AddWithValue("@First_Name", patientModel.First_Name);
                createCommand.Parameters.AddWithValue("@Last_Name", patientModel.Last_Name);
                createCommand.Parameters.AddWithValue("@Number", patientModel.Number);
                createCommand.Parameters.AddWithValue("@Email", patientModel.Email);
                createCommand.Parameters.AddWithValue("@ImageLink", patientModel.ImageLink);
                createCommand.Parameters.AddWithValue("@DOB", patientModel.DOB);
                createCommand.Parameters.AddWithValue("@Gender", patientModel.Gender);
                createCommand.Parameters.AddWithValue("@Contact_Address", patientModel.Contact_Address);
                createCommand.Parameters.AddWithValue("@Concern", patientModel.Concern);

                createCommand.ExecuteNonQuery();
                return true;
            }
        }

        public List<PatientModel> GetAllPatients()
        {
            List<PatientModel> patients = new List<PatientModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("usp_GetAllPatients", connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PatientModel patient = new PatientModel
                    {
                        Patient_Id = Convert.ToInt32(reader["Patient_Id"]),
                        IsTrash = Convert.ToBoolean(reader["Is_Trash"]),
                        First_Name = reader["First_Name"].ToString(),
                        Last_Name = reader["Last_Name"].ToString(),
                        Number = reader["Number"].ToString(),
                        Email = reader["Email"].ToString(),
                        ImageLink = reader["ImageLink"] != DBNull.Value ? reader["ImageLink"].ToString() : null,
                        DOB = Convert.ToDateTime(reader["DOB"]),
                        Age = Convert.ToInt32(reader["Age"]),
                        Gender = reader["Gender"].ToString(),
                        Contact_Address = reader["Contact_Address"].ToString(),
                        Concern = reader["Concern"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])
                    };
                    patients.Add(patient);
                }
            }
            return patients;
        }

        public PatientModel GetPatientById(int patientId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("usp_GetPatientById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Patient_Id", patientId);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    PatientModel patient = new PatientModel
                    {
                        Patient_Id = Convert.ToInt32(reader["Patient_Id"]),
                        //IsTrash = Convert.ToBoolean(reader["Is_Trash"]),
                        First_Name = reader["First_Name"].ToString(),
                        Last_Name = reader["Last_Name"].ToString(),
                        Number = reader["Number"].ToString(),
                        Email = reader["Email"].ToString(),
                        ImageLink = reader["ImageLink"] != DBNull.Value ? reader["ImageLink"].ToString() : null,
                        DOB = Convert.ToDateTime(reader["DOB"]),
                        Age = Convert.ToInt32(reader["Age"]),
                        Gender = reader["Gender"].ToString(),
                        Contact_Address = reader["Contact_Address"].ToString(),
                        Concern = reader["Concern"].ToString(),
                        //CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        //UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])
                    };
                    return patient;
                }
            }
            return null;
        }

        public bool UpdatePatient(PatientModel patientModel)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand updateCommand = new SqlCommand("usp_UpdatePatient", connection);
                updateCommand.CommandType = CommandType.StoredProcedure;

                updateCommand.Parameters.AddWithValue("@Patient_Id", patientModel.Patient_Id);
                updateCommand.Parameters.AddWithValue("@First_Name", patientModel.First_Name);
                updateCommand.Parameters.AddWithValue("@Last_Name", patientModel.Last_Name);
                updateCommand.Parameters.AddWithValue("@Number", patientModel.Number);
                updateCommand.Parameters.AddWithValue("@Email", patientModel.Email);
                updateCommand.Parameters.AddWithValue("@ImageLink", patientModel.ImageLink);
                updateCommand.Parameters.AddWithValue("@DOB", patientModel.DOB);
                updateCommand.Parameters.AddWithValue("@Gender", patientModel.Gender);
                updateCommand.Parameters.AddWithValue("@Contact_Address", patientModel.Contact_Address);
                updateCommand.Parameters.AddWithValue("@Concern", patientModel.Concern);

                updateCommand.ExecuteNonQuery();
                return true;
            }
        }

        public bool DeletePatient(int patientId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand deleteCommand = new SqlCommand("usp_DeletePatient", connection);
                deleteCommand.CommandType = CommandType.StoredProcedure;

                deleteCommand.Parameters.AddWithValue("@Patient_Id", patientId);

                deleteCommand.ExecuteNonQuery();
                return true;
            }
        }

        public LoginModel Login(LoginModel loginModel)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand loginCommand = new SqlCommand("usp_LoginPatient", connection);
                loginCommand.CommandType = CommandType.StoredProcedure;

                loginCommand.Parameters.AddWithValue("@Login_Id", loginModel.LoginId);
                loginCommand.Parameters.AddWithValue("@Email", loginModel.Email);

                using (SqlDataReader reader = loginCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        loginModel.LoginId = Convert.ToInt32(reader["Patient_Id"]);
                        loginModel.Email = reader["Email"].ToString();
                        return loginModel;
                    }
                }
            }
            return null;
        }

        public List<GetMyDoctorDetails> GetMyDoctorDetails(int PatientId)
        {
            List<GetMyDoctorDetails> myDoctorDetails = new List<GetMyDoctorDetails>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("usp_GetMyDoctorDetails", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Patient_Id", PatientId);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    GetMyDoctorDetails details = new GetMyDoctorDetails
                    {
                        PatientID = Convert.ToInt32(reader["Patient ID"]),
                        PatientName = reader["Patient Name"].ToString(),
                        DoctorID = Convert.ToInt32(reader["DoctorID"]),
                        DoctorName = reader["Doctor Name"].ToString(),
                        DoctorImage = reader["Doctor Image"].ToString(),
                        DoctorAge = Convert.ToInt32(reader["Doctor Age"]),
                        DoctorGender = reader["Gender"].ToString(),
                        Qualification = reader["Qualification"].ToString(),
                        Specialization = reader["Specialization"].ToString(),
                        YearsOfExperience = Convert.ToInt32(reader["Years_Of_Experience"])
                    };
                    myDoctorDetails.Add(details);
                }
            }
            return myDoctorDetails;
        }

    }
}

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
    public class DoctorRepo : IDoctorRepo
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
            if (doctorModel != null && connection != null)
            {
                connection.Open();
                SqlCommand createCommand = new SqlCommand("usp_CreateDoctorProfile", connection);
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

        public List<DoctorModel> GetAllDoctors()
        {
            List<DoctorModel> doctors = new List<DoctorModel>();

            try
            {
                connection.Open();
                SqlCommand getAllCommand = new SqlCommand("usp_GetAllDoctors", connection);
                getAllCommand.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = getAllCommand.ExecuteReader();
                while (reader.Read())
                {
                    DoctorModel doctor = new DoctorModel
                    {
                        DoctorId = Convert.ToInt32(reader["DoctorId"]),
                        IsTrash = Convert.ToBoolean(reader["IsTrash"]),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        ContactNumber = reader["ContactNumber"].ToString(),
                        Email = reader["Email"].ToString(),
                        ImageLink = reader["ImageLink"] != DBNull.Value ? reader["ImageLink"].ToString() : null,
                        DOB = Convert.ToDateTime(reader["DOB"]),
                        Age = Convert.ToInt32(reader["Age"]),
                        Gender = reader["Gender"].ToString(),
                        ContactAddress = reader["ContactAddress"].ToString(),
                        Qualification = reader["Qualification"].ToString(),
                        Specialization = reader["Specialization"].ToString(),
                        YearsOfExperience = Convert.ToInt32(reader["YearsOfExperience"]),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])
                    };
                    doctors.Add(doctor);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                //logger.LogError(ex, "An error occurred while fetching doctor details.");
                throw; // Optionally rethrow the exception to propagate it.
            }
            finally
            {
                connection.Close();
            }

            return doctors;
        }

        public DoctorModel GetDoctorById(int doctorId)
        {
            DoctorModel doctor = null;

            try
            {
                connection.Open();
                SqlCommand getByIdCommand = new SqlCommand("usp_GetDoctorById", connection);
                getByIdCommand.CommandType = CommandType.StoredProcedure;
                getByIdCommand.Parameters.AddWithValue("@DoctorId", doctorId);

                SqlDataReader reader = getByIdCommand.ExecuteReader();
                if (reader.Read())
                {
                    doctor = new DoctorModel
                    {
                        DoctorId = Convert.ToInt32(reader["DoctorId"]),
                        IsTrash = Convert.ToBoolean(reader["IsTrash"]),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        ContactNumber = reader["ContactNumber"].ToString(),
                        Email = reader["Email"].ToString(),
                        ImageLink = reader["ImageLink"] != DBNull.Value ? reader["ImageLink"].ToString() : null,
                        DOB = Convert.ToDateTime(reader["DOB"]),
                        Age = Convert.ToInt32(reader["Age"]),
                        Gender = reader["Gender"].ToString(),
                        ContactAddress = reader["Contact_Address"].ToString(),
                        Qualification = reader["Qualification"].ToString(),
                        Specialization = reader["Specialization"].ToString(),
                        YearsOfExperience = Convert.ToInt32(reader["Years_Of_Experience"]),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])
                    };
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                //logger.LogError(ex, "An error occurred while fetching doctor details by ID.");
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return doctor;
        }

        public bool UpdateDoctor(DoctorModel doctorModel)
        {
            try
            {
                if (doctorModel != null && connection != null)
                {
                    connection.Open();
                    SqlCommand updateCommand = new SqlCommand("usp_UpdateDoctor", connection);
                    updateCommand.CommandType = CommandType.StoredProcedure;

                    updateCommand.Parameters.AddWithValue("@DoctorId", doctorModel.DoctorId);
                    updateCommand.Parameters.AddWithValue("@FirstName", doctorModel.FirstName);
                    updateCommand.Parameters.AddWithValue("@LastName", doctorModel.LastName);
                    updateCommand.Parameters.AddWithValue("@ContactNumber", doctorModel.ContactNumber);
                    updateCommand.Parameters.AddWithValue("@Email", doctorModel.Email);
                    updateCommand.Parameters.AddWithValue("@ImageLink", doctorModel.ImageLink);
                    updateCommand.Parameters.AddWithValue("@DOB", doctorModel.DOB);
                    updateCommand.Parameters.AddWithValue("@Gender", doctorModel.Gender);
                    updateCommand.Parameters.AddWithValue("@Contact_Address", doctorModel.ContactAddress);
                    updateCommand.Parameters.AddWithValue("@Qualification", doctorModel.Qualification);
                    updateCommand.Parameters.AddWithValue("@Specialization", doctorModel.Specialization);
                    updateCommand.Parameters.AddWithValue("@Years_Of_Experience", doctorModel.YearsOfExperience);

                    updateCommand.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception ex)
            {
                //logger.LogError(ex, "An error occurred while fetching doctor details by ID.");
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return false;
            }

            public bool DeleteDoctor(int doctorId)
            {
                if (connection != null)
                {
                    connection.Open();
                    SqlCommand deleteCommand = new SqlCommand("usp_DeleteDoctor", connection);
                    deleteCommand.CommandType = CommandType.StoredProcedure;
                    deleteCommand.Parameters.AddWithValue("@DoctorId", doctorId);
                    deleteCommand.ExecuteNonQuery();
                    connection.Close();
                    return true;
                }
                return false;
            }
        }
    }

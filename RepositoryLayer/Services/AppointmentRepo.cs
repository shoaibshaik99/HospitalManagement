using Microsoft.Extensions.Configuration;
using ModelLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class AppointmentRepo: IAppointmentRepo
    {
        readonly IConfiguration config;
        readonly string connectionString;
        readonly SqlConnection connection = new SqlConnection();

        public AppointmentRepo(IConfiguration configuration)
        {
            config = configuration;
            connectionString = config.GetConnectionString("HospitalDBConnection");
            connection.ConnectionString = connectionString;
        }

        public bool CreateAppointment(AppointmentModel appointmentModel)
        {
            if (appointmentModel != null && connection != null)
            {
                connection.Open();

                // Validate Patient_Id and Doctor_Id
                if (!ValidatePatientId(appointmentModel.Patient_Id) || !ValidateDoctorId(appointmentModel.Doctor_Id))
                {
                    connection.Close();
                    return false;
                }

                SqlCommand createCommand = new SqlCommand("usp_CreateAppointment", connection);
                createCommand.CommandType = CommandType.StoredProcedure;

                createCommand.Parameters.AddWithValue("@Patient_Id", appointmentModel.Patient_Id);
                createCommand.Parameters.AddWithValue("@Doctor_Id", appointmentModel.Doctor_Id);
                createCommand.Parameters.AddWithValue("@Appointment_Date", appointmentModel.Appointment_Date);
                createCommand.Parameters.AddWithValue("@Start_Time", appointmentModel.Start_Time);
                createCommand.Parameters.AddWithValue("@Concerns", appointmentModel.Concerns);

                createCommand.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            return false;
        }

        private bool ValidatePatientId(int patientId)
        {
            SqlCommand command = new SqlCommand("SELECT COUNT(1) FROM Hospital.Patients WHERE Patient_Id = @Patient_Id", connection);
            command.Parameters.AddWithValue("@Patient_Id", patientId);
            return (int)command.ExecuteScalar() > 0;
        }

        private bool ValidateDoctorId(int doctorId)
        {
            SqlCommand command = new SqlCommand("SELECT COUNT(1) FROM Hospital.Doctors WHERE DoctorId = @Doctor_Id", connection);
            command.Parameters.AddWithValue("@Doctor_Id", doctorId);
            return (int)command.ExecuteScalar() > 0;
        }

        public List<AppointmentModel> GetAll()
        {
            var appointments = new List<AppointmentModel>();
            try
            {
                connection.Open();
                SqlCommand getAllCommand = new SqlCommand("usp_GetAllAppointments", connection);
                getAllCommand.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = getAllCommand.ExecuteReader();
                while (reader.Read())
                {
                    AppointmentModel model = new AppointmentModel();
                    model.Patient_Id = Convert.ToInt32(reader["Patient_Id"]);
                    model.Doctor_Id = Convert.ToInt32(reader["Doctor_Id"]);
                    model.Appointment_Date = Convert.ToDateTime(reader["Appointment_Date"]);
                    model.Start_Time = (TimeSpan)reader["Start_Time"];
                    model.End_Time = (TimeSpan)reader["End_Time"];
                    model.Concerns = reader["Concerns"].ToString();
                    appointments.Add(model);    
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
            return appointments;
        }

    }
}
